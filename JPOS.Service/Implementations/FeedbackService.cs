using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;

namespace JPOS.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackrepository;
        public FeedbackService(IFeedbackRepository feedbackrepository)
        {
            _feedbackrepository = feedbackrepository;
        }

        public async Task<bool> CreateFeedbackAsync(Feedback feedback)
        {
            var result = await _feedbackrepository.InsertAsync(feedback);
            return result;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var result = await _feedbackrepository.DeleteAsync(id);
            return result;
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await _feedbackrepository.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _feedbackrepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateFeedbackAsync(Feedback feedback)
        {
            var result = await _feedbackrepository.UpdateAsync(feedback);
            return result;
        }
    }
}
