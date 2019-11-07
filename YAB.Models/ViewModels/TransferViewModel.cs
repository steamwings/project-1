using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YAB.Models
{
    public class TransferViewModel
    {
        public Accounts AccountOutOf { get; set; }
        public Accounts AccountInto { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }
    }
}
