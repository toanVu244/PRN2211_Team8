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
    public class CatergoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryrepository;
        public CatergoryService(ICategoryRepository categoryrepository)
        {
            _categoryrepository = categoryrepository;
        }

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var result = await _categoryrepository.InsertAsync(category);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await _categoryrepository.DeleteAsync(id);
            return result;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _categoryrepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryrepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var result = await _categoryrepository.UpdateAsync(category);
            return result;
        }
    }
}
