
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
    public class FeedbackRepository : GenericRepository<Feedback>, IFeedbackRepository
    {
        private readonly JPOS_DatabaseContext _context;
        public FeedbackRepository(JPOS_DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Feedback> GetFeedbacByID(int Id)
        {
            return await _context.Feedbacks.FirstOrDefaultAsync(f => f.FeedBackId == Id);
        }
    }
}
