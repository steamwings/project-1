using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YAB.Models
{
    public abstract class Account
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] [Column(TypeName = "money")]
        public decimal Balance { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }

        public bool CompoundInterest(decimal rate, bool daily = false)
        {
            int timesToCompound; decimal divideBy = daily ? 356 : 12;
            if (daily)
            {
                double days = (DateTime.Now - LastUpdated).TotalDays;
                if (days < 1) return false;
                timesToCompound = (int)days;
            }
            else // Monthly
            {
                int months = LastUpdated.MonthsBefore(DateTime.Now);
                if (months < 1) return false;
                timesToCompound = months;
            }
            while (timesToCompound-- > 0)
            {
                decimal add = (Balance * rate) / divideBy;
                Balance += add;
                //Transactions.Add(new Transaction(TransactionType.InterestCompounded, add));
            }
            LastUpdated = DateTime.Now;
            return true;
        }

        protected Account(int id)
        {
            Id = id;
            LastUpdated = DateTime.Now;
            //Transactions.Add(new Transaction(TransactionType.OpenAccount));
        }

        public virtual string Info()
        {
            return $"Account Name: {Name}\nBalance: ${Balance.ToString("0.00")}\n";
        }
    }

    public class CheckingAccount : Account, IChecking
    {
        public CheckingAccount(int id) : base(id) { }

        public virtual void Deposit(decimal amount)
        {
            //Transactions.Add(new Transaction(TransactionType.Deposit, amount));
            Balance += amount;
        }

        public TransferResult TransferOut(decimal amount)
        {
            return Withdraw(amount);
        }

        public virtual TransferResult Withdraw(decimal amount)
        {
            TransferResult res;
            if (Balance < amount) res = TransferResult.InsufficientFunds;
            else
            {
                Balance -= amount;
                res = TransferResult.SuccessNoBorrow;
            }
           // Transactions.Add(new Transaction(TransactionType.TransferOutOf, amount, res));
            return res;
        }
    }

    public class BusinessAccount : CheckingAccount, IDebt
    {
        public BusinessAccount(int id) : base(id) { }

        public bool MakePayment(decimal payment)
        {
            Deposit(payment);
            return true;
        }

        public decimal AmountOwed()
        {
            if (Balance >= 0) return 0;
            else return (0 - Balance);
        }

        public override TransferResult Withdraw(decimal amount)
        {
            TransferResult res = TransferResult.SuccessNoBorrow;
            decimal diff = Balance - amount;
            if (diff < 0)
            {
                //Balance += (diff * InterestRate); //TODO
                res = TransferResult.SuccessBorrowing;
            }
            Balance -= amount;
           // Transactions.Add(new Transaction(TransactionType.TransferOutOf, amount, res));
            return res;
        }
    }

    public class Loan : Account, IDebt
    {
        public Loan(int id) : base(id)
        {
        }

        public void TakeOut(decimal amt)
        {
            // Negative balance indicates amount owed
            Balance -= amt;
        }

        public decimal AmountOwed()
        {
            return -Balance;
        }

        public bool MakePayment(decimal amount)
        {
            if (Balance < 0 && (Balance + amount) <= 0)
            {
                Balance += amount;
               // Transactions.Add(new Transaction(TransactionType.Payment, amount));
                return true;
            }
            return false;
        }
    }

    public class TermDeposit : Account, ITerm
    {
        public bool IsMature { get; set; } = false;
        public bool IsStarted { get; private set; } = false;
        public DateTime MaturityDate { get; private set; }
        public TermDeposit(int id) : base(id)
        {
        }
        public bool Start(decimal amt, DateTime maturity)
        {
            if (IsStarted) return false;
            Balance += amt;
            IsStarted = true;
            MaturityDate = maturity;
            return true;
        }

        public override string Info()
        {
            return base.Info() + (IsMature ? "(Maturation reached.)" : "(Maturation date has not been reached.)") + "\n";
        }

        public virtual TransferResult TransferOut(decimal amt)
        {
            TransferResult res;
            if (!IsMature)
            {
                res = TransferResult.ImmatureFunds;
            }
            else if (Balance < amt)
            {
                res = TransferResult.InsufficientFunds;
            }
            else
            {
                Balance -= amt;
                res = TransferResult.SuccessNoBorrow;
            }
            //Transactions.Add(new Transaction(TransactionType.TransferOutOf, amt, res));
            return res;
        }
    }
    
}
