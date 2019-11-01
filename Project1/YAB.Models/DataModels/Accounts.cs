using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YAB.Models
{
    public abstract class Account : IAccount
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Account Name")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        [Required]
        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; }
        [Required]
        [Display(Name = "Interest Rate")]
        public InterestRate InterestRate { get; set; }
        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }
    }

    public class CheckingAccount : Account, IChecking { }
    public class BusinessAccount : CheckingAccount, IDebt { }

    public class Loan : Account, IDebt
    {
        //[Required]
        //public LoanDetails LoanDetails { get; set; }
        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Payment Amount")]
        public decimal PaymentAmount { get; set; }
        [Required]
        [Display(Name = "Next Payment Due Date")]
        public DateTime NextPaymentDue { get; set; }
        [Required]
        [Display(Name = "Payments Behind")]
        public int PaymentsBehind { get; set; } = 0;
    }

    public class TermDeposit : Account, ITerm
    {
        //[Required]
        //public TermDeposit TermDetails { get; set; }
        [Required]
        [Display(Name = "Mature")]
        public bool IsMature { get; set; } = false;
        [Required]
        [Display(Name = "Started")]
        public bool IsStarted { get; set; } = false;
        [Required]
        [Display(Name = "Date of Maturity")]
        public DateTime MaturityDate { get; set; }
    }

    //public partial class TermDetails
    //{
    //    public bool IsMature { get; set; } = false;
    //    public bool IsStarted { get; private set; } = false;
    //    public DateTime MaturityDate { get; private set; }
    //}

    //public partial class LoanDetails
    //{
    //    public DateTime NextPaymentDue { get; set; }
    //    [Column(TypeName = "money")]
    //    public decimal PaymentAmount { get; set; }
    //    public int PaymentsBehind { get; set; }
    //}


}

