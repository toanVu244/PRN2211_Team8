using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.DesignPage
{
    public class IndexModel : PageModel
    {
        private readonly IDesignService _repo;

        public IndexModel(IDesignService _context)
        {
            _repo = _context;
        }

        public IList<Design> Design { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_repo.GetAllDesignAsync != null)
            {
                Design = await _repo.GetAllDesignAsync();
            }
        }
    }
}
