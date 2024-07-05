using JPOS.Model;
using JPOS.Model.Entities;
using JPOS.Model.Models.AppConfig;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppConfig _appConfig;

        public BlogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateBlogAsync(Blog blog)
        {
            var result = await _unitOfWork.Blogs.InsertAsync(blog);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            var result = await _unitOfWork.Blogs.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<List<Blog>> GetAllBlogAsync()
        {
            return await _unitOfWork.Blogs.GetAllAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int id)
        {
            return await _unitOfWork.Blogs.GetByIdAsync(id);
        }

        public async Task<bool> UpdateBlogAsync(Blog blog)
        {
            var result = await _unitOfWork.Blogs.UpdateAsync(blog);
            await _unitOfWork.CompleteAsync();
            return result;
        }
    }
}
