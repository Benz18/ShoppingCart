using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CA_Project.DB;

using CA_Project.Models;

namespace CA_Project.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index(string sessionId, ShoppingCart cart)
        {
            SessionData.RemoveSession(sessionId);
            cart.Clear(); 
            return RedirectToAction("Index", "Login");
        }
    }
}