using System;
using System.Collections.Generic;

namespace YAB.Models
{
    public interface IAccount
    {
        int Id { get; }
        string Name { get; set; }
        decimal Balance { get; set; }
        DateTime LastUpdated { get; set; }
        public InterestRate InterestRate { get; set; }
        public ICollection<CustomerAccount> CustomerAccounts { get; set; }
        //    ICollection<Transaction> Transactions { get; }
        // string Info();
        //  public bool CompoundInterest(decimal rate, bool daily = false);
    }

    public enum TransferResult
    {
        SuccessNoBorrow,
        SuccessBorrowing,
        InsufficientFunds,
        ImmatureFunds
    }

    // Accounts you can transfer from
    public interface ITransfer : IAccount
    {
     //   TransferResult TransferOut(decimal amt);
    }

    // Can deposit and withdraw
    public interface IChecking : ITransfer
    {
      //  void Deposit(decimal amt);
      //  TransferResult Withdraw(decimal amt);
    }

    // Accounts you can owe money on
    public interface IDebt : IAccount
    { 
     //   bool MakePayment(decimal payment);
      //  decimal AmountOwed();
    }

    public interface ITerm :  ITransfer
    {
        bool IsMature { get; set; }
    }
}
