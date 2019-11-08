using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAB.Models.Repos
{
    public interface IRepo<TModel, TKey>
    {
        public TModel Get(TKey id);
        public TModel GetAll(TKey id);
    }

    public interface ICustomerRepo { } //: IRepo<Customers> { }

    public interface IAccountsRepo //: IRepo<Accounts> 
    {
        public Task<List<Accounts>> GetAll(string userId);
    }

    public interface ITransactionsRepo { }
}
