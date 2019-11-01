using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YAB.Models
{
    public class InterestRate
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 5)")]
        [Display(Name = "Fractional Rate")]
        public decimal Rate { get; set; }
        [Required]
        [Display(Name = "Interest Type")]
        public string Name { get; set; }
    }
}
