using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
        public string TransRef { get; set; }
        public int UserID { get; set; }
    }
}
