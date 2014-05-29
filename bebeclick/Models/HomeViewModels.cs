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
        public IEnumerable<System.Web.Mvc.SelectListItem> States
        {
            get
            {
                var states = from state in StateEntity.GetAll()
                             select new System.Web.Mvc.SelectListItem
                             {
                                 Value = state.ID.ToString(),
                                 Text = state.Name
                             };

                return states;
            }
        }

        public Guid CityID { get; set; }

        public Guid StateID { get; set; }

        public Guid ProductID { get; set; }

        public Guid ServiceID { get; set; }

        public string Background { get; set; }
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