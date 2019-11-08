using System;
using System.Collections.Generic;
using System.Text;

namespace YAB.Models
{
    public class TransactionsViewModel
    {
        public DateTime Timestamp { get; set; }
        public decimal Amount { get; set; }
        public string TypeName { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
