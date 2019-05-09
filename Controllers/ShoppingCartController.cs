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
    public class ShoppingCartController : Controller
    {
        [PersonalClassFilter]
        // GET: ShoppingCart
        public ActionResult CartIndex(ShoppingCart cart, string sessionId, string returnUrl)
        {
            User user = UserData.GetUserBySessionId(sessionId);

            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;

            
            int sum = GetCartQuantity(cart);
            ViewData["quantity"] = sum;
            return View(new CartVM
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public ActionResult AddToCart(ShoppingCart cart, string sessionId, string productid, string keyword) 
        {
            Product product = ProductData.GetProductDetails().Where(p => p.ProductID == productid).FirstOrDefault();
            product.ImagePath = "/Image/" + product.ProductID + ".jpg";
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            int sum = GetCartQuantity(cart);
            ViewData["quantity"] = sum;
            return RedirectToAction("Index", "Product", new { sessionId, keyword, quantity = sum });
        }


        [HttpPost]
        public ActionResult IncreaseOrDecreaseOne(ShoppingCart cart, string sessionId, string productid, int quantity)
        {
            Product product = ProductData.GetProductDetails().Where(p => p.ProductID == productid).FirstOrDefault();
            if (product != null)
            {
                cart.IncreaseOrDecreaseOne(product, quantity);
            }
            ViewData["sessionId"] = sessionId;
            ViewData["cart"] = cart;

            return RedirectToAction("CartIndex","ShoppingCart", new { sessionId});
        }

        public ActionResult RemoveFromCart(ShoppingCart cart, string sessionId, string productid, string returnUrl)
        {
            Product product = ProductData.GetProductDetails().Where(p => p.ProductID == productid).FirstOrDefault();
            if (product != null)
            {
                cart.RemoveLine(product);
            }

            ViewData["sessionId"] = sessionId;
            return RedirectToAction("CartIndex", "ShoppingCart", new { sessionId, returnUrl });
        }

        [PersonalClassFilter]
        public ActionResult Checkout(ShoppingCart cart, string sessionId)
        {
            User user = UserData.GetUserBySessionId(sessionId);
            ViewData["user"] = user;
            ViewData["sessionId"] = sessionId;

            string tranid = DateTime.Now.ToString() + user.UserID; 
            string userid = user.UserID;
            DateTime dateTime = DateTime.Now;

            TransactionData.UpdateTransactions(tranid, userid, dateTime);

            foreach (CartDetail cartDetail in cart.details)
            {
                string tdid = DateTime.Now.ToString() + cartDetail.Product.ProductID;
                string pid = cartDetail.Product.ProductID;
                int _quantity = cartDetail.Quantity;
                TransactionData.UpdateTransactionDetails(tdid, tranid, pid, _quantity);
                for (int i = 0; i < _quantity; i++)
                {
                    string code = Guid.NewGuid().ToString();
                    TransactionData.UpdateProductkeys(tdid, tranid, pid, code);
                }
            }
            int Total_quantity = GetCartQuantity(cart);
            cart.Clear();
            return RedirectToAction("Receipt", "My_Purchases", new { sessionId , tranid, quantity = 0 });
        }

        public int GetCartQuantity(ShoppingCart cart)
        {
            int sum = 0;
            foreach (CartDetail cartDetail in cart.details)
            {
                sum += cartDetail.Quantity;
            }
            return sum;
        }
    }
    
}