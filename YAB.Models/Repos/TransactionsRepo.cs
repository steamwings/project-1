using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YAB.Models.Repos
{
    public class TransactionsRepo
    {
        private Project1Context _context;

        public TransactionsRepo(Project1Context c)
        {
            _context = c;
        }

        public int GetTypeId(string val)
        {
            return _context.TransactionTypes.Where(y => y.Name == val).Single().Id;
        }

        public string GetTypeName(int id)
        {
            return _context.TransactionTypes.Find(id).Name;
        }

        public void Add(string type, decimal amount, long? accountFromId = null, long? accountToId = null)
        {
            _context.Transactions.Add(new Transactions
            {
                Timestamp = DateTime.Now,
                TypeId = GetTypeId(type),
                Amount = amount,
                FromAccountId = accountFromId,
                ToAccountId = accountToId
            });
        }

    }
}
