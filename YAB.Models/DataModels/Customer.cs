using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace YAB.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Name { get { return FirstName + " " + LastName; } }
        // TODO: The tricky part is reflecting transactions in other customers who share accounts
        [Display(Name = "Transactions")]
        public virtual ICollection<Transaction> Transactions { get; set; }
        [Display(Name = "Account Links")]
        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }

    }
}
