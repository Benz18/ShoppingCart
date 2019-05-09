using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Project.Models
{
    public class CartVM
    {
        public ShoppingCart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}