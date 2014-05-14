using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bebeclick.Models
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
    }

    public class StateViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

    }
}