using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Diagnostics;
using CA_Project.Models;

namespace CA_Project.DB
{
    public class UserData : Data
    {
        public static User GetUserByUsername(string username)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(Data.connectionString))
            {
                conn.Open();

                string sql = @"SELECT UserID, Username, Password from Account WHERE Username = '" + username + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        UserID = (string)reader["UserID"],
                        Username = (string)reader["Username"],
                        Password = (string)reader["Password"]
                    };
                }
            }
            return user;
        }
        public static User GetUserBySessionId(string sessionId)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string q = @"SELECT UserID, First_name, 
                    Last_name FROM Account WHERE SessionID = '" + sessionId + "'";

                SqlCommand cmd = new SqlCommand(q, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    user = new User()
                    {
                        UserID = (string)reader["UserID"],
                        FirstName = (string)reader["First_name"],
                        LastName = (string)reader["Last_name"]
                    };
                }
            }
            return user;
        }
    }
}