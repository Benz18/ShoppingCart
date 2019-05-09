using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CA_Project.Models;
using System.Web.Mvc;

namespace CA_Project.Extensions
{
    public class CartModelBinder: IModelBinder
    {
        private const string sessionKey = "Cart";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ShoppingCart cart = (ShoppingCart)controllerContext.HttpContext.Session[sessionKey];
            if (cart == null)
            {
                cart = new ShoppingCart();
                controllerContext.HttpContext.Session[sessionKey] = cart;
            }
            return cart;
        }
    }
}