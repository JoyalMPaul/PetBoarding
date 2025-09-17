using Microsoft.AspNet.Identity;
using PetBoarding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetBoarding.Controllers
{
    [Authorize]
    [RequireHttps]
    public class MyAccountController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();

            using (var db = new ApplicationDbContext())
            {
                var ownerInfo = db.Users
                    .OfType<OwnerModel>()       
                    .FirstOrDefault(o => o.Id == userId);

                if (ownerInfo == null)
                {
                    return HttpNotFound("Owner not found.");
                }

                return View(ownerInfo);
            }
        }
    }
}