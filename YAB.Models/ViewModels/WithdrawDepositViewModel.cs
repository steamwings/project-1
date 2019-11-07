using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public class WithdrawDepositViewModel
    {
        public long AccountId { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}
