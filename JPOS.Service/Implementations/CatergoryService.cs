using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models.AppConfig;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class CatergoryService:ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CatergoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var result = await _unitOfWork.Categories.InsertAsync(category);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _unitOfWork.Categories.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Categories.GetByIdAsync(id);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var result = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
