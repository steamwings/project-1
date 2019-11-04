using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YetAnotherBankWeb.Models
{
    public partial class Customers
    {
        public Customers()
        {
            CustomersToAccounts = new HashSet<CustomersToAccounts>();
        }

        public long Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] Salt { get; set; }

        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
    }
}
