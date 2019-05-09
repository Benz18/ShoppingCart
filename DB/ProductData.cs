using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CA_Project.Models;
using System.Data.SqlClient;

namespace CA_Project.DB
{
    public class ProductData : Data
    {
        public static List<Product> GetProductDetailsByKeyword(string keyword)
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql;
                if (keyword == "")
                {
                    sql = @"SELECT ProductID, ProductName, Description, UnitPrice FROM Product";
                }
                else
                {
                    sql = @"SELECT ProductID, ProductName, Description, UnitPrice FROM Product WHERE Description Like '%" + keyword + "%'";
                }
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = (string)reader["ProductID"],
                        ProductName = (string)reader["ProductName"],
                        Description = (string)reader["Description"],
                        UnitPrice = (double)reader["UnitPrice"],
                        ImagePath = "/Image/" + (string)reader["ProductID"] + ".jpg"
                    };

                    products.Add(_product);
                }
            }

            return products;
        }
        public static List<Product> GetProductDetails()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql;
                sql = @"SELECT ProductID, ProductName, Description, UnitPrice FROM Product";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Product _product = new Product()
                    {
                        ProductID = (string)reader["ProductID"],
                        ProductName = (string)reader["ProductName"],
                        Description = (string)reader["Description"],
                        UnitPrice = (double)reader["UnitPrice"]
                    };

                    products.Add(_product);
                }
            }
            return products;
        }
    }
}