/*using JPOS.Model.Entities;
using JPOS.Model.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Model.Repositories.Implementations
{
    public class BlogRepository: GenericRepository<Blog>, IBlogRepository
    {
        private readonly JPOS_ProjectContext _context;

        public BlogRepository(JPOS_ProjectContext context):base(context)
        {
            _context = context;
        }

        public async Task<Blog> GetBlogByID(int blogID)
        {
            return await _context.Blogs.FirstOrDefaultAsync(b => b.BlogId == blogID);
        }
    }
}
*/