using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAB.Models.Repos
{

    public class AccountsRepo : IAccountsRepo
    {
        private Project1Context _context;
        public AccountsRepo(Project1Context c)
        {
            _context = c;
        }

        public bool Exists(string userId, long accId)
        {
            return UserAccounts(userId).Where(a => a.Id == accId).Count() > 0;
        }

        public Task<int> Count(string userId)
        {
            return UserAccounts(userId).CountAsync();
        }

        public Task<Accounts> Get(string userId, long accId)
        {
            return _context.Accounts.Intersect(UserAccounts(userId)).Where(a => a.Id == accId)
                .Include(a => a.Interest)
                .Include(a => a.Type)
                .Include(a => a.IncomingTransactions)
                .Include(a => a.OutgoingTransactions)
                .Include(a => a.DebtAccounts)
                .Include(a => a.TermAccounts)
                .FirstOrDefaultAsync();
            //return UserAccounts(userId).Where(a => a.Id == accId).FirstOrDefaultAsync();
        }
        public Task<List<Accounts>> GetAll(string userId)
        {
            return _context.Accounts.Intersect(UserAccounts(userId)).Include(a => a.Interest).Include(a => a.Type).ToListAsync();
            //TODO: More efficient?
            //return UserAccounts(userId).Include(a => a.Interest).Include(a => a.Type).ToListAsync();
        }

        private IQueryable<Accounts> UserAccounts(string userId)
        {
            return _context.CustomersToAccounts.Where(ca => ca.CustomerId == userId).Select(ca => ca.Account);
            //TODO: More efficient?
            //return _context.Customers.Find(userId).CustomersToAccounts.Select(ca => ca.Account);
        }

    }
}
