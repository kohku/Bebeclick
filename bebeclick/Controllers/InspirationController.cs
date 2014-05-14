using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bebeclick.Models;

namespace Bebeclick.Controllers
{
    public class InspirationController : Controller
    {
        // GET: Inspiration
        public ActionResult Index()
        {
            var model = new InspirationIndexViewModel();
            return View(model);
        }
    }
}