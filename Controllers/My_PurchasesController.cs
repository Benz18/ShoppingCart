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
    public class My_PurchasesController : Controller
    {
        [PersonalClassFilter]
        // GET: My_Purchases
        public ActionResult Index(string sessionId, int quantity)
        {
            User user = UserData.GetUserBySessionId(sessionId);

            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;
            List<Tran_Details> tran_Details = TransactionData.GetTransactionDetails(user.UserID);
            ViewData["quantity"] = quantity;
            ViewData["tran_Details"] = tran_Details;
            return View();
        }

        public ActionResult Receipt(string sessionId, string tranid, int quantity)
        {
            User user = UserData.GetUserBySessionId(sessionId);

            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;
            ViewData["quantity"] = quantity;
            List<Tran_Details> tran_Details = TransactionData.GetTransactionDetailsByTranID(tranid);

            ViewData["tran_Details"] = tran_Details;
            return View("Index");
        }
    }
}