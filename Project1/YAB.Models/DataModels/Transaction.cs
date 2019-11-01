using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace YAB.Models
{
    public enum TransactionType
    {
        Deposit,
        Withdraw,
        TransferOutOf,
        TransferInto,
        Payment,
        OpenAccount,
        CloseAccount,
        InterestCompounded
    }
    public class Transaction
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Transaction Type")]
        public TransactionType Type { get; set; }
        [DisplayFormat(NullDisplayText = "Not a transfer.")]
        [Display(Name = "Transfer Result")]
        public TransferResult? TransferResult { get; set; }
        [Required]
        [Column(TypeName = "money")]
        [Display(Name = "Transaction Amount")]
        public decimal Amount { get; set; }
        [Display(Name = "Transaction Time")]
        public DateTime Time { get; set; }
        [Required]
        [Display(Name = "Source Account")]
        public virtual Account AccountFrom { get; set; }
        [Display(Name = "Destination Account")]
        public virtual Account AccountTo { get; set; }

        //public Transaction()
        //{

        //}

        //public Transaction(TransactionType t)
        //{
        //    Type = t;
        //}
        //public Transaction(TransactionType t, decimal amt)
        //{
        //    Type = t;
        //    Amount = amt;
        //}

        //public Transaction(TransactionType t, decimal amt, TransferResult tr)
        //{
        //    Type = t;
        //    TransferResult = tr;
        //    Amount = amt;
        //}

        //public (string time, string transaction, string amt, string transfer) Values()
        //{
        //    return (Time.ToString(), Enum.GetName(typeof(TransactionType), Type), Amount.ToString("0.00"),
        //        $"{Enum.GetName(typeof(TransferResult), TransferResult)}");
        //}

        //public override string ToString()
        //{
        //    var vals = Values();
        //    return $"{vals.time}\t{vals.transaction}\t{vals.amt}\t{vals.transfer}";
        //}

        //public static string FormatTransactions(List<Transaction> list)
        //{
        //    List<ValueTuple<string,string,string,string>> vals = list.Select(t => t.Values()).ToList();
        //    List<int> offsets = new List<int>(4);
        //    offsets.Add(4+vals.Select(v => v.Item1).Max(s => s.Length));
        //    offsets.Add(4+vals.Select(v => v.Item2).Max(s => s.Length));
        //    offsets.Add(4+vals.Select(v => v.Item3).Max(s => s.Length));
        //    offsets.Add(4+vals.Select(v => v.Item4).Max(s => s.Length));
        //    return string.Join(Environment.NewLine, vals.Select(
        //        vt => vt.Item1.PadRight(offsets[0]) + vt.Item2.PadRight(offsets[1]) 
        //        + vt.Item3.PadRight(offsets[2]) + vt.Item4.PadRight(offsets[3])
        //    ));
        //}
    }
}
