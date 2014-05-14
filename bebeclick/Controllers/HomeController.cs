using Bebeclick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bebeclick.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();

            ViewBag.Title = "Bebeclick";

            return View(model);
        }

        public ActionResult About()
        {
            var model = new HomeAboutViewModel();

            return View(model);
        }

        public ActionResult Contact()
        {
            var model = new HomeContactViewModel();

            return View(model);
        }

        public ActionResult Terms()
        {
            var model = new HomeTermsViewModel();

            return View(model);
        }
    }
}