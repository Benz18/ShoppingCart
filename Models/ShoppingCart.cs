using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using CA_Project.Models;
using CA_Project.DB;

namespace CA_Project.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartID { get; set; }
        private List<CartDetail> cartitem = new List<CartDetail>();
       
        public void AddItem(Product product, int quantity)
        {
            CartDetail cartDetails = cartitem.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (cartDetails == null)
            {
                cartitem.Add(new CartDetail() { Product = product, Quantity = quantity });
            }
            else
            {
                cartDetails.Quantity += quantity;
            }
        }
        
        public void IncreaseOrDecreaseOne(Product product, int quantity)
        {
            CartDetail cartDetails = cartitem.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
            if (cartDetails != null)
            {
                cartDetails.Quantity = quantity;
            }
        }
        
        public void RemoveLine(Product product)
        {
            cartitem.RemoveAll(p => p.Product.ProductID == product.ProductID);
        }
        
        public double ComputeTotalPrice()
        {
            return cartitem.Sum(p => p.Product.UnitPrice * p.Quantity);
        }
        
        public void Clear()
        {
            cartitem.Clear();
        }
        
        public IEnumerable<CartDetail> details
        {
            get { return cartitem; }
        }
    }
}