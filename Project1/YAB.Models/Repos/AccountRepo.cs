using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YAB.Models.Repos
{
    public class AccountRepo
    {
        private readonly P1Context _context;
        public AccountRepo(P1Context c)
        {
            _context = c;
        }

        public async Task<Account> Get(int? id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}
