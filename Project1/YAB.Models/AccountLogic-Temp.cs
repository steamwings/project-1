using System;

namespace YAB.Models
{
    //public abstract partial class Account
    //{
    //    public bool CompoundInterest(decimal rate, bool daily = false)
    //    {
    //        int timesToCompound; decimal divideBy = daily ? 356 : 12;
    //        if (daily)
    //        {
    //            double days = (DateTime.Now - LastUpdated).TotalDays;
    //            if (days < 1) return false;
    //            timesToCompound = (int)days;
    //        }
    //        else // Monthly
    //        {
    //            int months = LastUpdated.MonthsBefore(DateTime.Now);
    //            if (months < 1) return false;
    //            timesToCompound = months;
    //        }
    //        while (timesToCompound-- > 0)
    //        {
    //            decimal add = (Balance * rate) / divideBy;
    //            Balance += add;
    //            //Transactions.Add(new Transaction(TransactionType.InterestCompounded, add));
    //        }
    //        LastUpdated = DateTime.Now;
    //        return true;
    //    }

    //    protected Account(int id)
    //    {
    //        Id = id;
    //        LastUpdated = DateTime.Now;
    //        //Transactions.Add(new Transaction(TransactionType.OpenAccount));
    //    }

    //    public virtual string Info()
    //    {
    //        return $"Account Name: {Name}\nBalance: ${Balance.ToString("0.00")}\n";
    //    }
    //}

    //public partial class CheckingAccount : Account, IChecking
    //{
    //    public CheckingAccount(int id) : base(id) { }

    //    public virtual void Deposit(decimal amount)
    //    {
    //        //Transactions.Add(new Transaction(TransactionType.Deposit, amount));
    //        Balance += amount;
    //    }

    //    public TransferResult TransferOut(decimal amount)
    //    {
    //        return Withdraw(amount);
    //    }

    //    public virtual TransferResult Withdraw(decimal amount)
    //    {
    //        TransferResult res;
    //        if (Balance < amount) res = TransferResult.InsufficientFunds;
    //        else
    //        {
    //            Balance -= amount;
    //            res = TransferResult.SuccessNoBorrow;
    //        }
    //       // Transactions.Add(new Transaction(TransactionType.TransferOutOf, amount, res));
    //        return res;
    //    }
    //}

    //public partial class BusinessAccount : CheckingAccount, IDebt
    //{
    //    public BusinessAccount(int id) : base(id) { }

    //    public bool MakePayment(decimal payment)
    //    {
    //        Deposit(payment);
    //        return true;
    //    }

    //    public decimal AmountOwed()
    //    {
    //        if (Balance >= 0) return 0;
    //        else return (0 - Balance);
    //    }

    //    public override TransferResult Withdraw(decimal amount)
    //    {
    //        TransferResult res = TransferResult.SuccessNoBorrow;
    //        decimal diff = Balance - amount;
    //        if (diff < 0)
    //        {
    //            //Balance += (diff * InterestRate); //TODO
    //            res = TransferResult.SuccessBorrowing;
    //        }
    //        Balance -= amount;
    //       // Transactions.Add(new Transaction(TransactionType.TransferOutOf, amount, res));
    //        return res;
    //    }
    //}

    //public partial class Loan : Account, IDebt
    //{
    //    public Loan(int id) : base(id)
    //    {
    //    }

    //    public void TakeOut(decimal amt)
    //    {
    //        // Negative balance indicates amount owed
    //        Balance -= amt;
    //    }

    //    public decimal AmountOwed()
    //    {
    //        return -Balance;
    //    }

    //    public bool MakePayment(decimal amount)
    //    {
    //        if (Balance < 0 && (Balance + amount) <= 0)
    //        {
    //            Balance += amount;
    //           // Transactions.Add(new Transaction(TransactionType.Payment, amount));
    //            return true;
    //        }
    //        return false;
    //    }
    //}

    //public partial class TermDeposit : Account, ITerm
    //{

    //    public TermDeposit(int id) : base(id)
    //    {
    //    }
    //    public bool Start(decimal amt, DateTime maturity)
    //    {
    //        if (IsStarted) return false;
    //        Balance += amt;
    //        IsStarted = true;
    //        MaturityDate = maturity;
    //        return true;
    //    }

    //    public override string Info()
    //    {
    //        return base.Info() + (IsMature ? "(Maturation reached.)" : "(Maturation date has not been reached.)") + "\n";
    //    }

    //    public virtual TransferResult TransferOut(decimal amt)
    //    {
    //        TransferResult res;
    //        if (!IsMature)
    //        {
    //            res = TransferResult.ImmatureFunds;
    //        }
    //        else if (Balance < amt)
    //        {
    //            res = TransferResult.InsufficientFunds;
    //        }
    //        else
    //        {
    //            Balance -= amt;
    //            res = TransferResult.SuccessNoBorrow;
    //        }
    //        //Transactions.Add(new Transaction(TransactionType.TransferOutOf, amt, res));
    //        return res;
    //    }


    // Customer stuff
    //public Customer(string email)
    //{
    //    Email = email;
    //}

    //public int AccountCount()
    //{
    //    return Accounts.Count();
    //}
    //public void AddAccount(Account a)
    //{
    //    Accounts.Add(a);
    //}
    //public void RemoveAccount(Account a)
    //{
    //    Accounts.Remove(a);
    //}

    //public void RemoveAllAccounts()
    //{
    //    Accounts.Clear();
    //}
    //public string DisplayAllAccounts()
    //{
    //    StringBuilder info = new StringBuilder();
    //    foreach(IAccount a in Accounts)
    //    {
    //        info.Append(a.Info());
    //    }
    //    return info.ToString();
    //}

    //public bool HasDebt()
    //{
    //    foreach(IAccount a in Accounts)
    //    {
    //        if (a.Balance < 0) return true;
    //    }
    //    return false;
    //}

    //public bool HasFunds()
    //{
    //    foreach (IAccount a in Accounts)
    //    {
    //        if (a.Balance > 0) return true;
    //    }
    //    return false;
    //}

    //public List<TAccount> GetAccounts<TAccount>() where TAccount : Account
    //{
    //    List<TAccount> list = new List<TAccount>();
    //    foreach(var account in Accounts)
    //    {
    //        if (account is TAccount) list.Add((TAccount)account);
    //    }
    //    return list;
    //}

    //public List<string> GetAccountNames<TAccount>() where TAccount : Account
    //{
    //    return (from a in Accounts where a is TAccount select a.Name).ToList();
    //}

    //public bool HasAccount(string name)
    //{
    //    if (name == null) return false;
    //    return Accounts.Where(a => a.Name == name).Any();
    //}

    //public IAccount GetAccount(string name)
    //{
    //    return Accounts.Where(a => a.Name == name).Single();
    //}

    //}





}
