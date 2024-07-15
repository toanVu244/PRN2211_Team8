using JPOS.Model;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Model.Entities;

namespace JPOS.Service.Implementations
{
    public class TransactionServices : ITransactionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateTransaction(Transaction transaction)
        {
            var result = await _unitOfWork.Transactions.InsertAsync(transaction);
            await _unitOfWork.CompleteAsync();
            return result;

        }

        public async Task<List<Transaction?>> GetTransactionByUserID(string id)
        {
            return await _unitOfWork.Transactions.GetTransactionByUserID(id);
        }
    }
}
