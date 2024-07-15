using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Service.Implementations;

namespace JPOS.Controller.Pages.Dashboard.Blogs
{
    public class IndexModel : PageModel
    {
        private readonly IBlogService _blogService;
        private readonly IUserServices _userService;

        public IndexModel(IBlogService blogService, IUserServices userService)
        {
            _blogService = blogService;
            _userService = userService;
        }

        public IList<BlogViewModel> Blogs { get; set; } = new List<BlogViewModel>();

        public async Task OnGetAsync()
        {
            var blogs = await _blogService.GetAllBlogAsync();
            var users = await _userService.GetAllUsersAsync();

            Blogs = blogs.Select(blog => new BlogViewModel
            {
                BlogId = blog.BlogId,
                Title = blog.Title,
                Content = blog.Content,
                CreateDate = blog.CreateDate,
                CreatedByName = users.FirstOrDefault(u => u.UserId == blog.CreateBy)?.FullName
            }).ToList();
        }

        public class BlogViewModel
        {
            public int BlogId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public DateTime? CreateDate { get; set; }
            public string CreatedByName { get; set; }
        }
    }
}
