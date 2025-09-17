using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetBoarding.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AdminController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}