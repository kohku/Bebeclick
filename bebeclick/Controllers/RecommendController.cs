using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bebeclick.Models;

namespace Bebeclick.Controllers
{
    public class RecommendController : Controller
    {
        // GET: Recommend
        public ActionResult Index()
        {
            var model = new RecommendIndexViewModel();
            return View(model);
        }
    }
}