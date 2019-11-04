using System;
using System.Collections.Generic;
using YetAnotherBankWeb.Models;

namespace YetAnotherBankWeb
{
    public partial class Accounts
    {
        public Accounts()
        {
            CustomersToAccounts = new HashSet<CustomersToAccounts>();
            DebtAccounts = new HashSet<DebtAccounts>();
            TermAccounts = new HashSet<TermAccounts>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public int InterestId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool Active { get; set; }

        public virtual InterestRates Interest { get; set; }
        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
        public virtual ICollection<DebtAccounts> DebtAccounts { get; set; }
        public virtual ICollection<TermAccounts> TermAccounts { get; set; }
    }
}
