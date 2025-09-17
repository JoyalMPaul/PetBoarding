using Microsoft.AspNet.Identity;
using PetBoarding.Models;
using PetBoarding.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml.Linq;

namespace PetBoarding.Controllers
{
    [Authorize]
    [RequireHttps]
    public class PetsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var userID = User.Identity.GetUserId();

                var petIds = dbContext.PetToOwner
                .Where(x => x.OwnerId == userID)
                .Select(x => x.PetID)
                .ToList();

                var pets = dbContext.PetModels
                    .Where(p => petIds.Contains(p.PetID))
                    .ToList();

                return View(pets);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PetAddVM petViewModels)
        {
            if (ModelState.IsValid)
            {
                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    var pet = new PetModel
                    {
                        Name = petViewModels.Name,
                        Gender = petViewModels.Gender,
                        Breed = petViewModels.Breed,
                        Age = petViewModels.Age,
                        EmergencyContactNumber = petViewModels.EmergencyContact,
                        DietaryInstructions = petViewModels.DietaryInstructions,
                    };
                    dbContext.PetModels.Add(pet);
                    dbContext.SaveChanges();
                    
                    var link = new PetToOwnerModel
                    {
                        PetID = pet.PetID,
                        OwnerId = User.Identity.GetUserId()
                    };
                    dbContext.PetToOwner.Add(link);
                    dbContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View(petViewModels);
        }

        [HttpGet]
        public ActionResult Edit(Guid? petID)
        {
            if (petID == null) { return RedirectToAction("Index"); }

            var userID = User.Identity.GetUserId();
            using (var dbContext = new ApplicationDbContext())
            {
                bool ownsPet = dbContext.PetToOwner
                .Any(x => x.PetID == petID && x.OwnerId == userID);

                if (!ownsPet) { return new HttpUnauthorizedResult(); }

                var pet = dbContext.PetModels.FirstOrDefault(p => p.PetID == petID);
                if (pet == null) { return RedirectToAction("Index"); }

                PetEditVM petEditVM = new PetEditVM
                {
                    PetID = pet.PetID,
                    Name = pet.Name,
                    Gender = pet.Gender,
                    Breed = pet.Breed,
                    Age = pet.Age,
                    EmergencyContact = pet.EmergencyContactNumber,
                    DietaryInstructions = pet.DietaryInstructions
                };
                return View(petEditVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PetEditVM petEditVM)
        {
            var userID = User.Identity.GetUserId();
            using (var dbContext = new ApplicationDbContext())
            {
                bool ownsPet = dbContext.PetToOwner
                .Any(x => x.PetID == petEditVM.PetID && x.OwnerId == userID);

                if (!ownsPet) { return new HttpUnauthorizedResult(); }
                
                var pet = dbContext.PetModels.FirstOrDefault(p => p.PetID == petEditVM.PetID);
                if (pet == null) { return RedirectToAction("Index"); }

                pet.Name = petEditVM.Name;
                pet.Gender = petEditVM.Gender;
                pet.Breed = petEditVM.Breed;
                pet.Age = petEditVM.Age;
                pet.EmergencyContactNumber = petEditVM.EmergencyContact;
                pet.DietaryInstructions = petEditVM.DietaryInstructions;

                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Remove(Guid id)
        {
            var userID = User.Identity.GetUserId();
            using (var dbContext = new ApplicationDbContext())
            {
                PetModel pet = dbContext.PetModels.FirstOrDefault(x => x.PetID == id);

                if (pet != null)
                {
                    bool ownsPet = dbContext.PetToOwner
                    .Any(x => x.PetID == id && x.OwnerId == userID);

                    if (!ownsPet) { return new HttpUnauthorizedResult(); }

                    // Removes bookings for pet
                    List<BookingsModel> bookings_list = pet.Bookings.ToList();
                    foreach (var booking in bookings_list)
                    {
                        dbContext.BookingsModels.Remove(booking);
                    }

                    // Removes PetToOwner relationship for pet
                    var link = dbContext.PetToOwner.FirstOrDefault(l => l.PetID == pet.PetID && l.OwnerId == userID);
                    if (link != null)
                    {
                        dbContext.PetToOwner.Remove(link);
                    }

                    dbContext.PetModels.Remove(pet);
                    dbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return Content("Pet does not exist");
                }
            }
        }
    }
}