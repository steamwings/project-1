using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public class WithdrawDepositViewModel
    {
        [Required]
        public Accounts Account { get; set; }
        [DataType(DataType.Currency)]
        [Required]
        public decimal Amount { get; set; }
    }
}
