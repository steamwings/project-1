using System;
using System.Collections.Generic;

namespace YetAnotherBankWeb
{
    public partial class Customers
    {
        public Customers()
        {
            CustomersToAccounts = new HashSet<CustomersToAccounts>();
        }

        public long Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
    }
}
