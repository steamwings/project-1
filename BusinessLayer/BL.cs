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

        public BL(Project1Context c)
        {
            _context = c;
        }


        public async Task<List<Accounts>> GetChecking(Project1Context c, string userId)
        {
            var _repo = new AccountsRepo(c);
            List<Accounts> all = await _repo.GetAll(userId);
            return all.Where(a=> a.Type.Id == _repo.GetTypeId("Checking")).ToList();
        }

    }
}
