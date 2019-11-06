using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public partial class CustomersToAccounts
    {
        public long Id { get; set; }
        [Required]
        public long AccountId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        public virtual Accounts Account { get; set; }
        public virtual Customers Customer { get; set; }
    }
}
