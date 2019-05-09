using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Project.Models
{
    public class Transaction
    {
        public string TransactionID { get; set; }
        public string UserID { get; set; }
        public string TransactionDate { get; set; }
        public List<Tran_Details> Tran_DetaiL { get ; set ; }
    }
}