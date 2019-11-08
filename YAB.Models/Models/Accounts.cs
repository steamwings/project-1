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
        [Required]
        [MinLength(4, ErrorMessage = "Name cannot be less than 4 characters.")]
        public string Name { get; set; }        
        [Required]
        public AccountTypes Type { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Balance { get; set; }
        [Required]
        public int InterestId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime Created { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name="Updated")]
        public DateTime LastUpdated { get; set; }
        [Required]
        [UIHint("_BoolAccount")]
        [Display(Name="Status")]
        public bool Active { get; set; }
        [Required]
        [Display(Name="Use")]
        [UIHint("_BoolBusiness")]
        public bool Business { get; set; }

        public virtual InterestRates Interest { get; set; }
        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
        // 0 <= *total debt+term accts* <= 1
        public virtual ICollection<DebtAccounts> DebtAccounts { get; set; }
        public virtual ICollection<TermAccounts> TermAccounts { get; set; }

        public virtual ICollection<Transactions> IncomingTransactions { get; set; }
        public virtual ICollection<Transactions> OutgoingTransactions { get; set; }
    }
}
