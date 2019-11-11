using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public partial class InterestRates
    {
        public InterestRates()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayFormat(DataFormatString ="{0:0.00000}")]
        public decimal Rate { get; set; }
        public int? Years { get; set; }
        [Required]
        public AccountTypes Type { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
