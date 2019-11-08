using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YAB.Models
{
    public class PaymentViewModel
    {
        public long AccountId;
        public long DebtAccountId;
        [RegularExpression(@"^\d*[^0\D]\d*(\.\d{2})?$|^\d*\.(\d[^0\D]|[^0\D]\d)$", ErrorMessage = "Must be positive dollar amount.")]
        public decimal Amount;
    }
}
