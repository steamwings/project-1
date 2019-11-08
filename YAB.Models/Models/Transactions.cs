using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YAB.Models
{

    public class TransactionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Transactions> Transactions { get; set; }
    }

    public class Transactions
    {
        public long Id { get; set; }
        [Required]
        public DateTime Timestamp { get; set; }
        [Required]
        public int TypeId { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        public long? FromAccountId { get; set; }
        public long? ToAccountId { get; set; }

        public virtual TransactionType Type { get; set; }
        public virtual Accounts FromAccount { get; set; }
        public virtual Accounts ToAccount { get; set; }
    }
}
