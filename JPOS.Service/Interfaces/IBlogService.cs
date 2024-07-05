using JPOS.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Interfaces
{
    public interface IBlogService
    {
        public Task<List<Blog>> GetAllBlogAsync();
        public Task<Blog> GetBlogByIdAsync(int id);
        public Task<bool> CreateBlogAsync(Blog blog);
        public Task<bool> UpdateBlogAsync(Blog blog);
        public Task<bool> DeleteBlogAsync(int id);
    }
}
