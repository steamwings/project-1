using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YAB.Models
{
    public class TransferViewModel
    {
        [Display(Name ="Transfer from")]
        public long AccountFromId { get; set; }
        [Display(Name ="Transfer into")]
        public long AccountToId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d*[^0\D]\d*(\.\d{2})?$|^\d*\.(\d[^0\D]|[^0\D]\d)$", ErrorMessage = "Must be positive dollar amount.")]
        public decimal Amount { get; set; }
    }
}
