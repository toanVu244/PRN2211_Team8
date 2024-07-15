using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Service.Implementations;

namespace JPOS.Controller.Pages.Dashboard.BlogPage
{
    public class IndexModel : PageModel
    {
        private readonly IBlogService _repo;
        private readonly IUserServices userservice;

        public IndexModel(IBlogService _service, IUserServices userservice)
        {
            _repo = _service;
            this.userservice = userservice;
        }

        public IList<Blog> Blog { get;set; } = default!;
        public IList<User> Users { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task callData()
        {
            Users = await userservice.GetAllUsersAsync();
            Blog = await _repo.GetAllBlogAsync();
        }

        public async Task OnGetAsync()
        {
            await callData();
        }
        public async Task OnPost()
        {
            await callData();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Blog = Blog.Where(p => p.BlogId.Equals(SearchTerm) || p.CreateBy.Contains(SearchTerm)).ToList();
            }
        }
    }
}
