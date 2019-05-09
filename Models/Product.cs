using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Project.Models
{
    public class Product
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        public string ImagePath { get; set; }

    }
}