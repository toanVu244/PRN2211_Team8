using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;
using JPOS.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Repository.Repositories.Implementations
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private static CategoryRepository _instance;

        public static CategoryRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CategoryRepository();
                }
                return _instance;
            }
        }

        public async Task<Category> GetCategoryByID(int cateId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.CatId == cateId);
        }
    }
}
