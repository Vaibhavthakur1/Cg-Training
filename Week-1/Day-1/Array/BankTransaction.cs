using System;
using System.Collections.Generic;
using System.Text;

namespace DSA_Array
{
    internal class BankTransaction
    {
        public string AccountId { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime Timestamp { get; set; }
        public string MerchantName { get; set; }
    }
}
