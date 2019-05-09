using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using CA_Project.DB;
using CA_Project.Models;
using System.Diagnostics;
using CA_Project.Extensions;
namespace CA_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(string Username, string Password)
        {
            User user = UserData.GetUserByUsername(Username);
            if (user == null)
                return View();
            if (user.Password != Encryptor.MD5Hash(Password))
                return View();

            string sessionId = SessionData.CreateSession(user.UserID);
            return RedirectToAction("Index", "Product", new { sessionId, quantity = 0 }); 
        }
    }
}