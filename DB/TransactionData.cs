using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CA_Project.Models;
using System.Data.SqlClient;

namespace CA_Project.DB
{
    public class TransactionData : Data
    {
        public static List<Tran_Details> GetTransactionDetails(string userid)
        {
            List<string> transactions = new List<string>();
            List<Tran_Details> tran_Details = new List<Tran_Details>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql1, sql2, sql3;
                sql1 = @"SELECT TransactionID
                FROM Transactions,Account
                WHERE Account.UserID = Transactions.UserID AND Account.UserID = '" + userid + "'";
                sql2 = @"SELECT TranDe_ID,Product.ProductID as ProductID,ProductName,Description,Quantity,Transaction_Date
                FROM Transactions,Transaction_Details,Product
                WHERE Transaction_Details.TransactionID = Transactions.TransactionID 
                AND Product.ProductID = Transaction_Details.ProductID AND Transactions.TransactionID = ";
                sql3 = @"SELECT Activation_code
                FROM Transaction_Details,[Product key]
                WHERE[Product key].TranDe_ID = Transaction_Details.TranDe_ID
                AND Transaction_Details.TranDe_ID =";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    string TransactionID = (string)reader1["TransactionID"];
                    transactions.Add(TransactionID);
                }
                reader1.Close();
                foreach (string trans in transactions)
                {
                    string sql = sql2 + "'" + trans + "'";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);

                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        Tran_Details tran_Detail = new Tran_Details()
                        {
                            TranDe_ID = (string)reader2["TranDe_ID"],
                            ProductID = (string)reader2["ProductID"],
                            ProductName = (string)reader2["ProductName"],
                            Description = (string)reader2["Description"],
                            TransactionDate = (DateTime)reader2["Transaction_Date"],
                            Quantity = (int)reader2["Quantity"]
                        };

                        tran_Details.Add(tran_Detail);
                    }
                    reader2.Close();
                }

                foreach (Tran_Details tran_Detail in tran_Details)
                {
                    List<string> codes = new List<string>();
                    string sql = sql3 + "'" + tran_Detail.TranDe_ID + "'";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);

                    SqlDataReader reader3 = cmd2.ExecuteReader();
                    while (reader3.Read())
                    {
                        string code = (string)reader3["Activation_code"];

                        codes.Add(code);
                    }
                    reader3.Close();
                    tran_Detail.Activation_Codes = codes;
                }

                conn.Close();
            }
            return tran_Details;
        }
        public static void UpdateTransactions(string tranid, string userid, DateTime dateTime)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql1;
                sql1 = @"INSERT INTO Transactions VALUES('" + tranid + "','" + userid + "','" + dateTime + "')";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
         }
        public static void UpdateTransactionDetails(string tdid, string tranid, string pid, int quantity)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql1;
                sql1 = @"INSERT INTO Transaction_Details VALUES('" + tdid + "','" + tranid + "','" + pid + "'," + quantity + ")";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }
        public static void UpdateProductkeys(string tdid, string tranid, string pid, string code)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql1;
                sql1 = @"INSERT INTO [Product key] VALUES('" + tdid + "','" + tranid + "','" + pid + "','" + code + "')";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);
                cmd1.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static List<Tran_Details> GetTransactionDetailsByTranID(string tranid)
        {
            
            List<Tran_Details> tran_Details = new List<Tran_Details>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql1, sql2;
                sql1 = @"SELECT TranDe_ID,Product.ProductID AS ProductID,ProductName,Description,Quantity,Transaction_Date
                FROM Transactions,Transaction_Details,Product
                WHERE Transaction_Details.TransactionID = Transactions.TransactionID 
                AND Product.ProductID = Transaction_Details.ProductID AND Transactions.TransactionID = '" + tranid + "'";
                sql2 = @"SELECT Activation_code
                FROM Transaction_Details,[Product key]
                WHERE[Product key].TranDe_ID = Transaction_Details.TranDe_ID
                AND Transaction_Details.TranDe_ID =";
                
                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                SqlDataReader reader1 = cmd1.ExecuteReader();
                while (reader1.Read())
                {
                    Tran_Details tran_Detail = new Tran_Details()
                    {
                        TranDe_ID = (string)reader1["TranDe_ID"],
                        ProductID = (string)reader1["ProductID"],
                        ProductName = (string)reader1["ProductName"],
                        Description = (string)reader1["Description"],
                        TransactionDate = (DateTime)reader1["Transaction_Date"],
                        Quantity = (int)reader1["Quantity"]
                    };

                    tran_Details.Add(tran_Detail);
                }
                reader1.Close();

                foreach (Tran_Details tran_Detail in tran_Details)
                {
                    List<string> codes = new List<string>();
                    string sql = sql2 + "'" + tran_Detail.TranDe_ID + "'";
                    SqlCommand cmd2 = new SqlCommand(sql, conn);

                    SqlDataReader reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        string code = (string)reader2["Activation_code"];

                        codes.Add(code);
                    }
                    reader2.Close();
                    tran_Detail.Activation_Codes = codes;
                }
                conn.Close();
            }
            return tran_Details;
        }
    }
}