using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bebeclick.Models;

namespace Bebeclick.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            var model = new SearchIndexViewModel();
            return View(model);
        }
    }
}