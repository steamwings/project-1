using System;
using System.Collections.Generic;
using System.Text;

namespace YAB.Models
{
    public class TransactionsViewModel
    {
        public ICollection<Transactions> InTx { get; set; }
        public ICollection<Transactions> OutTx { get; set; }
    }
}
