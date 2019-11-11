using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAB.Models;
using YAB.Models.Repos;

namespace YAB.BusinessLayer
{
    public partial class BL
    {
        private Project1Context _context = null;
        private readonly AccountsRepo _arepo;

        public BL(Project1Context c)
        {
            _context = c;
            _arepo = new AccountsRepo(c);
        }

        public Accounts CreateAccount(NewAccountViewModel vm)
        {
            if (_context is null) return null;
            Accounts a = new Accounts();
            
            return a;
        }

        public async Task<List<Accounts>> GetChecking(string userId)
        {
            List<Accounts> all = await _arepo.GetAll(userId);
            return all.Where(a=> a.Type.Id == _arepo.GetTypeId("Checking")).ToList();
        }

    }
}
