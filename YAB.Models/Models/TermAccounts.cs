using System;
using System.Collections.Generic;

namespace YAB.Models
{
    public partial class TermAccounts
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public DateTime MaturationDate { get; set; }

        public virtual Accounts Account { get; set; }
    }
}
