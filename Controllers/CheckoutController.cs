using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CA_Project.Models;
using CA_Project.DB;
using CA_Project.Filters;

namespace CA_Project.Controllers
{
    public class CheckoutController : Controller
    {
        [PersonalClassFilter]
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }
    }
}