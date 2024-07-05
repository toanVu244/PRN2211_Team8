using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IFeedbackService
    {
        public Task<List<Feedback>> GetAllFeedbackAsync();
        public Task<Feedback> GetFeedbackByIdAsync(int id);
        public Task<bool> CreateFeedbackAsync(Feedback feedback);
        public Task<bool> UpdateFeedbackAsync(Feedback feedback);
        public Task<bool> DeleteFeedbackAsync(int id);
    }
}
