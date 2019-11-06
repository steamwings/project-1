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

        public Accounts Get(int id)
        {
            return _context.Accounts.Find(id);
        }

        public IQueryable<Accounts> GetQueryable()
        {
            return _context.Accounts.Include(a => a.Interest);
        }
    }
}
