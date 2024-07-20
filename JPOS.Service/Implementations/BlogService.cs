using BusinessObject.Entities;
using BusinessObject.Entities;
using JPOS.Repository.Repositories.Implementations;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPOS.Service.Implementations
{
    public class BlogService : IBlogService
    {
        /*private readonly IBlogRepository _blogrepository;

        public BlogService(IBlogRepository blogrepository)
        {
            _blogrepository = blogrepository;
        }*/

        public async Task<bool> CreateBlogAsync(Blog blog)
        {
            var result = await BlogRepository.Instance.InsertAsync(blog);
            return result;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var result = await BlogRepository.Instance.DeleteAsync(id);
            return result;
        }

        public async Task<List<Blog>> GetAllBlogAsync()
        {
            return await BlogRepository.Instance.GetAllAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await BlogRepository.Instance.GetByIdAsync(id);
        }

        public async Task<bool> UpdateBlogAsync(Blog blog)
        {
            var result = await BlogRepository.Instance.UpdateAsync(blog);
            return result;
        }
    }
}
