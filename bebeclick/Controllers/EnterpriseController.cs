using Bebeclick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bebeclick.Controllers
{
    public class EnterpriseController : Controller
    {
        // GET: Enterprise
        public ActionResult Index()
        {
            var model = new EnterpriseIndexViewModel();
            return View(model);
        }
    }
}