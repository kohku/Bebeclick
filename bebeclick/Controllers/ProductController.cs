﻿using Bebeclick.Models;
using Bebeclick.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bebeclick.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search()
        {
            var query = from p in Product.GetAll()
                        where p.Visible
                        select new ProductViewModel
                        {
                            ID =  p.ID,
                            Name = p.Name
                        };

            return Json(query, JsonRequestBehavior.AllowGet);
        }
    }
}