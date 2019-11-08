using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YAB.Models
{
    public class NewAccountViewModel
    {
        public string Name { get; set; }
        [Display(Prompt = "Is this a business account?")]
        public bool Business { get; set; }
        [Display(Name = "Account Type")]
        public int TypeId { get; set; }
        [Display(Name = "Interest Rate")]
        public int InterestId { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "Loan Sum", Prompt = "What is the size of the loan?")]
        [RegularExpression(@"^\d*[^0\D]\d*(\.\d{2})?$|^\d*\.(\d[^0\D]|[^0\D]\d)$", ErrorMessage = "Must be positive dollar amount.")]
        public decimal? Amount { get; set; }
    }
}
