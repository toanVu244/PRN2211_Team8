using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface ITransactionServices
    {
        public Task<List<Transaction?>> GetTransactionByUserID(string id);
        public Task<bool> CreateTransaction(Transaction transaction);
    }
}
