using Bebeclick.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bebeclick.Models
{
    public class HomeIndexViewModel
    {
        public SelectList State
        {
            get
            {
                var query = from p in Product.GetAll()
                            where p.Visible
                            select new ProductViewModel
                            {
                                ID = p.ID,
                                Name = p.Name
                            };

                return new SelectList(query.ToArray(), "ID", "Name");
            }
        }
    }
}