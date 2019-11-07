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
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }
    }
}
