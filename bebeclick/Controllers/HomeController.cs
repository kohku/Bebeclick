using Bebeclick.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using System.Threading.Tasks;
using Bebeclick.WebClient;
using Bebeclick.WebClient.Utilities;


namespace Bebeclick.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeIndexViewModel();

            var files = System.IO.Directory.GetFiles(Server.MapPath("~/Content/images/home/"));

            model.Background = string.Format("~/Content/images/home/{0}", System.IO.Path.GetFileName(files.RandomElement()));

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

        [AllowAnonymous]
        public ActionResult GetCities(Guid stateId)
        {
            var cities = from province in Province.GetProvinces(stateId)
                         select new { value = province.ID, text = province.Name };

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetProducts(Guid cityId)
        {
            var cities = from product in Province.Load(cityId).Products
                         select new { value = product.ID, text = product.Name };

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetServices(Guid productId)
        {
            var cities = from service in Service.GetServices(productId)
                         select new { value = service.ID, text = service.Name };

            return Json(cities, JsonRequestBehavior.AllowGet);
        }
    }
}