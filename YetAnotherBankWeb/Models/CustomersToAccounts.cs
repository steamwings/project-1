using System;
using System.Collections.Generic;

namespace YetAnotherBankWeb.Models
{
    public partial class CustomersToAccounts
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public long CustomerId { get; set; }

        public virtual Accounts Account { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
