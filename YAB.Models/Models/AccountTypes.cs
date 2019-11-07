using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public class AccountTypes
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<InterestRates> InterestRates { get; set; }
    }
}
