using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetBoarding.Models;
using PetBoarding.ViewModels;

namespace PetBoarding.Controllers
{
    [RequireHttps]
    public class ContactUsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new ContactUsSubmissionVM());
        }

        [HttpGet]
        public ActionResult SuccessfulSubmit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ContactUsSubmissionVM contactUsSubmissionVM)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            ContactUsModel contactUsModel = new ContactUsModel
            {
                FirstName = contactUsSubmissionVM.FirstName,
                LastName = contactUsSubmissionVM.LastName,
                Email = contactUsSubmissionVM.Email,
                PhoneNumber = contactUsSubmissionVM.PhoneNumber,
                ReasonForContact = contactUsSubmissionVM.ReasonForContact,
                BasicInfo = contactUsSubmissionVM.BasicInfo,
            };

            try
            {
                dbContext.ContactUsModels.Add(contactUsModel);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("SuccessfulSubmit");
        }
    }
}