using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CA_Project.Models
{
    public class Tran_Details
    {
        public string TranDe_ID { get; set; }
        public string ProductID { get; set; }
        public string TransactionID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public List<string> Activation_Codes { get ; set ; }
    }
}