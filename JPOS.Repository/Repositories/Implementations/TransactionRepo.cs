
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class TransactionRepo : GenericRepository<Transaction>, ITransactionRepo
    {
        private static TransactionRepo _instance;

        public static TransactionRepo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TransactionRepo();
                }
                return _instance;
            }
        }

        public async Task<List<Transaction?>> GetTransactionByUserID(string id)
        {
            return await _context.Transactions.Where(t => t.UserId == id).ToListAsync();
        }
    }
}
