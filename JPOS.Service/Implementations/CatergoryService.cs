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
    public class CatergoryService : ICategoryService
    {
        /*private readonly ICategoryRepository _categoryrepository;
        public CatergoryService(ICategoryRepository categoryrepository)
        {
            _categoryrepository = categoryrepository;
        }*/

        public async Task<bool> CreateCategoryAsync(Category category)
        {
            var result = await CategoryRepository.Instance.InsertAsync(category);
            return result;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var result = await CategoryRepository.Instance.DeleteAsync(id);
            return result;
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await CategoryRepository.Instance.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await CategoryRepository.Instance.GetByIdAsync(id);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            var result = await CategoryRepository.Instance.UpdateAsync(category);
            return result;
        }
    }
}
