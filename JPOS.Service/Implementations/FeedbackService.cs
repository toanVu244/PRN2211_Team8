using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        public FeedbackService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateFeedbackAsync(Feedback feedback)
        {
            var result = await _unitOfWork.Feedbacks.InsertAsync(feedback);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteFeedbackAsync(int id)
        {
            var result = await _unitOfWork.Feedbacks.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            return await _unitOfWork.Feedbacks.GetAllAsync();
        }

        public async Task<Feedback> GetFeedbackByIdAsync(int id)
        {
            return await _unitOfWork.Feedbacks.GetByIdAsync(id);
        }

        public async Task<bool> UpdateFeedbackAsync(Feedback feedback)
        {
            var result = await _unitOfWork.Feedbacks.UpdateAsync(feedback);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
