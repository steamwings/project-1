using System;
using System.Collections.Generic;
using System.Text;
using YetAnotherBankWeb;

namespace YAB.Models.Repos
{
    public class CustomersRepo
    {
        private Project1Context _context;
        public CustomersRepo(Project1Context c)
        {
            _context = c;
        }
    }
}
