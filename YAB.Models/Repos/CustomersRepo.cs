using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async void Add(string id)
        {
            await _context.Customers.AddAsync(new Customers() { Id = id });
            await _context.SaveChangesAsync();
        }

        public ValueTask<Customers> Get(string id)
        {
            return _context.Customers.FindAsync(id);
        }

        public async Task<bool> Delete(string id)
        {
            if (id is null) return false;
            var c = _context.Customers.Find(id);
            if (c is null) return false;
            _context.Customers.Remove(c);
            await _context.SaveChangesAsync();
            return true;
        }

    }

}


