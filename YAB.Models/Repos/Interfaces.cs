using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAB.Models.Repos
{
    //public interface IRepo<T>
    //{
    //    public T Get(int id);
    //}

    public interface ICustomerRepo { } //: IRepo<Customers> { }

    public interface IAccountsRepo //: IRepo<Accounts> 
    {
        public IQueryable<Accounts> GetQueryable(string userId);
    }
}
