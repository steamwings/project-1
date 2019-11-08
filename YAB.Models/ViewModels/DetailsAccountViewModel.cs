using System;
using System.Collections.Generic;
using System.Text;

namespace YAB.Models
{
    public class DetailsAccountViewModel
    {
        public Accounts Accounts { get; set; }
        public IEnumerable<TransactionsViewModel> TransactionsVMs { get; set; }
    }
}
