using System;
using System.Collections.Generic;

namespace YAB.Models
{
    public partial class InterestRates
    {
        public InterestRates()
        {
            Accounts = new HashSet<Accounts>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }

        public virtual ICollection<Accounts> Accounts { get; set; }
    }
}
