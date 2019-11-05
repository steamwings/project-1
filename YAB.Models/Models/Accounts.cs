using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
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
        [MinLength(4, ErrorMessage = "Name cannot be less than 4 characters.")]
        public string Name { get; set; }        
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        public int InterestId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime LastUpdated { get; set; }
        public bool Active { get; set; }
        public bool Business { get; set; }

        public virtual InterestRates Interest { get; set; }
        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
        // 0 <= *total debt+term accts* <= 1
        public virtual ICollection<DebtAccounts> DebtAccounts { get; set; }
        public virtual ICollection<TermAccounts> TermAccounts { get; set; }
    }
}
