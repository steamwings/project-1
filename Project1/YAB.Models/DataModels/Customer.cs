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
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<CustomerAccount> CustomerAccounts { get; set; }
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

    }
}
