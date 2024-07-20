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
    public class BlogRepository : GenericRepository<Blog>, IBlogRepository
    {
        private static BlogRepository _instance;

        public static BlogRepository Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlogRepository();
                }
                return _instance;
            }
        }

        public async Task<Blog> GetBlogByID(int blogID)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogID);
        }
    }
}
