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
        public SelectList States
        {
            get
            {
                var query = from s in StateEntity.GetAll()
                            where s.Visible
                            select new 
                            {
                                ID = s.ID,
                                Name = s.Name
                            };

                return new SelectList(query.ToArray(), "ID", "Name");
            }
        }

        public SelectList Products
        {
            get
            {
                var query = from p in Product.GetAll()
                            where p.Visible
                            select new
                            {
                                ID = p.ID,
                                Name = p.Name
                            };

                return new SelectList(query.ToArray(), "ID", "Name");
            }
        }

        public SelectList Services
        {
            get
            {
                var query = from s in Service.GetAll()
                            where s.Visible
                            select new
                            {
                                ID = s.ID,
                                Name = s.Name
                            };

                return new SelectList(query.ToArray(), "ID", "Name");
            }
        }
    }

    public class HomeContactViewModel
    {
        
    }

    public class HomeAboutViewModel
    {
        
    }

    public class HomeTermsViewModel
    {
        
    }
}