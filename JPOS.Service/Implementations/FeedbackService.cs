using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Repository.Repositories.Implementations;

namespace JPOS.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        /*private readonly IFeedbackRepository _feedbackrepository;
        public FeedbackService(IFeedbackRepository feedbackrepository)
        {
            _feedbackrepository = feedbackrepository;
        }*/

        public async Task<bool> CreateFeedbackAsync(Feedback feedback)
        {
            var result = await FeedbackRepository.Instance.InsertAsync(feedback);
            return result;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var result = await FeedbackRepository.Instance.DeleteAsync(id);
            return result;
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await FeedbackRepository.Instance.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await FeedbackRepository.Instance.GetByIdAsync(id);
        }

        public async Task<bool> UpdateFeedbackAsync(Feedback feedback)
        {
            var result = await FeedbackRepository.Instance.UpdateAsync(feedback);
            return result;
        }
    }
}
