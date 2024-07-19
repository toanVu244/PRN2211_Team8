
using BusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Interfaces
{
    public interface IFeedbackRepository : IGenericRepository<Feedback>
    {
        public Task<Feedback> GetFeedbacByID(int Id);
    }
}
