using Microsoft.AspNet.Identity;
using PetBoarding.Models;
using PetBoarding.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace PetBoarding.Controllers
{
    [Authorize]
    [RequireHttps]
    public class BookingsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();

            using (var dbContext = new ApplicationDbContext())
            {
                var petIds = dbContext.PetToOwner
                .Where(x => x.OwnerId == userID)
                .Select(x => x.PetID)
                .ToList();

                var bookings = dbContext.BookingsModels
                    .Include(b => b.Pet)
                    .Where(b => petIds.Contains(b.Pet.PetID))
                    .ToList();

                return View(bookings);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            var userID = User.Identity.GetUserId();
            var model = new BookingsVM();
            using (var dbContext = new ApplicationDbContext())
            {
                var pets = dbContext.PetModels
                .Where(p => dbContext.PetToOwner.Any(po => po.PetID == p.PetID && po.OwnerId == userID))
                .ToList();

                if (!pets.Any()) { return RedirectToAction("Index"); }

                model.AvailablePets = pets.Select(p => new SelectListItem
                {
                    Value = p.PetID.ToString(),
                    Text = p.Name
                }).ToList();

                if (model.AvailablePets == null) { return RedirectToAction("Index"); }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(BookingsVM bookingsVM)
        {
            if (ModelState.IsValid)
            {
                var userID = User.Identity.GetUserId();
                using (var dbContext = new ApplicationDbContext())
                {
                    bool ownsPet = dbContext.PetToOwner.Any(x => x.PetID == bookingsVM.PetID && x.OwnerId == userID);
                    if (!ownsPet)
                    {
                        ModelState.AddModelError("PetID", "You do not own the selected pet.");
                        return View(bookingsVM);
                    }

                    var pet = dbContext.PetModels.FirstOrDefault(p => p.PetID == bookingsVM.PetID);
                    if (pet == null)
                    {
                        ModelState.AddModelError("PetID", "Selected pet does not exist.");
                        return View(bookingsVM);
                    }

                    var booking = new BookingsModel
                    {
                        Pet = pet,
                        BookInTime = bookingsVM.BookInTime,
                        BookOutTime = bookingsVM.BookOutTime,
                        RoomNumber = new Random().Next(10, 100).ToString(),
                        CareTakerID = new Random().Next(100, 1000)
                    };

                    dbContext.BookingsModels.Add(booking);
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(bookingsVM);
        }

        [HttpGet]
        public ActionResult Remove(Guid id)
        {
            var userID = User.Identity.GetUserId();
            using (var dbContext = new ApplicationDbContext())
            {
                var booking = dbContext.BookingsModels.FirstOrDefault(x => x.BookingID == id);
                if (booking == null)
                {
                    return Content("Null");
                }

                bool ownsPet = dbContext.PetToOwner.Any(x => x.PetID == booking.Pet.PetID && x.OwnerId == userID);
                if (!ownsPet)
                {
                    return new HttpUnauthorizedResult();
                }

                dbContext.BookingsModels.Remove(booking);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}