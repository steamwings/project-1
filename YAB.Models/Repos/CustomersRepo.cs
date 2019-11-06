using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YetAnotherBankWeb;

namespace YAB.Models.Repos
{
    public class CustomersRepo : ICustomerRepo
    {
        private Project1Context _context;
        public CustomersRepo(Project1Context c)
        {
            _context = c;
        }

        public Customers Get(int id)
        {
            return _context.Customers.Find(id);
        }

    }

}


