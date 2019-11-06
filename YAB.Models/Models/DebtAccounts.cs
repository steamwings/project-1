using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public partial class DebtAccounts
    {
        public long Id { get; set; }
        [Required]
        public long AccountId { get; set; }
        [Required]
        public decimal PaymentAmount { get; set; }
        [Required]
        public int PaymentsBehind { get; set; }
        public DateTime NextPaymentDue { get; set; }
        public virtual Accounts Account { get; set; }
    }
}
