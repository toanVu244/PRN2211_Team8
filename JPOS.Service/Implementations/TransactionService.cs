using JPOS.Model;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Entities;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Repository.Repositories.Implementations;

namespace JPOS.Service.Implementations
{
    public class TransactionServices : ITransactionServices
    {
        /*private readonly ITransactionRepo _transactionRepo;

        public TransactionServices(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }*/

        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            var result = await TransactionRepo.Instance.InsertAsync(transaction);
            return result;

        }

        public async Task<List<Transaction?>> GetTransactionByUserID(string id)
        {
            return await TransactionRepo.Instance.GetTransactionByUserID(id);
        }
    }
}
