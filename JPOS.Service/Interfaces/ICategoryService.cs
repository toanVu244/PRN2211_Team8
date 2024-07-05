using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetAllCategoryAsync();
        public Task<Category> GetCategoryByIdAsync(int id);
        public Task<bool> CreateCategoryAsync(Category category);
        public Task<bool> UpdateCategoryAsync(Category category);
        public Task<bool> DeleteCategoryAsync(int id);
    }
}
