using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YAB.Models
{
    public partial class Customers
    {
        public Customers()
        {
            CustomersToAccounts = new HashSet<CustomersToAccounts>();
        }
        [Key]
        public string Id { get; set; }
        public virtual ICollection<CustomersToAccounts> CustomersToAccounts { get; set; }
    }
}
