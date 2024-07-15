using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.BlogPage
{
    public class IndexModel : PageModel
    {
        private readonly IBlogService _repo;

        public IndexModel(IBlogService _service)
        {
            _repo = _service;
        }

        public IList<Blog> Blog { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_repo.GetAllBlogAsync != null)
            {
                Blog = await _repo.GetAllBlogAsync();
            }
        }
    }
}
