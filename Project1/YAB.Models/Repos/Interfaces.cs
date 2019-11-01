using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace YAB.Models.Repos
{
    public interface IRepo<T>
    {
        public Task<T> Get(int? id);
    }

    public interface ICustomerRepo : IRepo<Customer> { }


}
