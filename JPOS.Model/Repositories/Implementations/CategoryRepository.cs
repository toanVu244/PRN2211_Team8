using JPOS.Model.Entities;
using JPOS.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly JPOS_ProjectContext _context;

        public CategoryRepository(JPOS_ProjectContext context):base(context) 
        {
            _context = context;
        }

        public async Task<Category> GetCategoryByID(int cateId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CatId == cateId);
        }
    }
}
