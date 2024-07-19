
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
        private readonly JPOS_DatabaseContext _context;

        public BlogRepository(JPOS_DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Blog> GetBlogByID(int blogID)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogID);
        }
    }
}
