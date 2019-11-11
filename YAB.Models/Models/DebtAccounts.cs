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
        [DataType(DataType.Currency)]
        [Display(Name ="Payment Amount")]
        public decimal PaymentAmount { get; set; }
        [Required]
        [Display(Name="Payments Owed")]
        public int PaymentsBehind { get; set; }
        [Display(Name = "Next Payment Due")]
        public DateTime NextPaymentDue { get; set; }
        public virtual Accounts Account { get; set; }
    }
}
