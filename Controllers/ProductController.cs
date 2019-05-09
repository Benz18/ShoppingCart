using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using CA_Project.Models;
using CA_Project.DB;
using CA_Project.Filters;

namespace CA_Project.Controllers
{
    public class ProductController : Controller
    {
        [PersonalClassFilter]
        
        // GET: Product
        public ActionResult Index(string sessionId, string keyword,int quantity)
        {
            User user = UserData.GetUserBySessionId(sessionId);

            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;
            ViewData["keyword"] = keyword;  
            List<Product> products = ProductData.GetProductDetailsByKeyword(keyword);
            ViewData["product"] = products;
            ViewData["quantity"] = quantity;
            return View();
        }
    }
}