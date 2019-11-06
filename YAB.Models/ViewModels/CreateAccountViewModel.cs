using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YAB.Models
{
    public class CreateAccountViewModel
    {
        public int InterestId { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Loan Sum", Prompt="What is the size of the loan?")]
        public decimal? LoanAmount { get; set; }
        [Display(Name="Term Length (Years)", Prompt="How many years is your investment?")]
        public int? TermYears { get; set; }
    }
}
