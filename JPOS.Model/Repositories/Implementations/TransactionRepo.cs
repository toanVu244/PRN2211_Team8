using JPOS.Model.Entities;
using JPOS.Model.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Implementations
{
    public class TransactionRepo : GenericRepository<Transaction>, ITransactionRepo
    {
        private readonly JPOS_ProjectContext _context;

        public TransactionRepo(JPOS_ProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
