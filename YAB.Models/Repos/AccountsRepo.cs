using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAB.Models.Repos
{

    public class AccountsRepo : IAccountsRepo
    {
        private Project1Context _context;
        public AccountsRepo(Project1Context c)
        {
            _context = c;
        }

        private IQueryable<Accounts> UserAccounts(string userId)
        {
            return _context.CustomersToAccounts.Where(ca => ca.CustomerId == userId).Select(ca => ca.Account);
        }

        public Accounts Get(string userId, int accId)
        {
            return UserAccounts(userId).Where(a => a.Id == accId).Single();
        }

        public IQueryable<Accounts> GetQueryable(string userId)
        {
            return _context.Accounts.Intersect(UserAccounts(userId)).Include(a => a.Interest);
        }
    }
}
