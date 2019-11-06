using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YAB.Models
{
    public class NewAccountViewModel
    {
        public string Name { get; set; }
        [Display(Prompt = "Is this a business account?")]
        public bool Business { get; set; }
        [Display(Name = "Account Type")]
        public AccountTypes Type { get; set; }
    }
}
