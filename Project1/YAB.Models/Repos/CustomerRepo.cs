using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YAB.Models;

namespace YAB.Models.Repos
{
    public class CustomerRepo : ICustomerRepo
    {
        private P1Context _context;

        public CustomerRepo(P1Context c)
        {
            _context = c;
        }
        
        public async Task<Customer> Get(int? id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}
