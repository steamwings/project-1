using System;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public partial class TermAccounts
    {
        public long Id { get; set; }
        [Required]
        public long AccountId { get; set; }
        [Required]
        [Display(Name ="Matures on")]
        public DateTime MaturationDate { get; set; }

        public virtual Accounts Account { get; set; }
    }
}
