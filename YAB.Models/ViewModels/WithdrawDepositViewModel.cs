using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public class WithdrawDepositViewModel
    {
        public long AccountId { get; set; }
        [DataType(DataType.Currency)]
        [RegularExpression(@"^\d*[^0\D]\d*(\.\d{2})?$|^\d*\.(\d[^0\D]|[^0\D]\d)$", ErrorMessage = "Must be positive dollar amount.")]
        public decimal Amount { get; set; }
    }
}
