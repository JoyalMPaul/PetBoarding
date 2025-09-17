using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetBoarding.Controllers
{
    [RequireHttps]
    public class HoursController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}