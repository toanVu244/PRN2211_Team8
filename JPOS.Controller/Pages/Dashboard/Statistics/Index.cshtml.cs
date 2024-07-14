using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;

namespace JPOS.Controller.Pages.Dashboard.Statistics
{
    public class IndexModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public IndexModel(JPOS.Model.Entities.JPOS_ProjectContext context)
        {
            _context = context;
        }

        public IList<Request> Request { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Requests != null)
            {
                Request = await _context.Requests
                .Include(r => r.Product)
                .Include(r => r.User).ToListAsync();
            }
        }
    }
}
