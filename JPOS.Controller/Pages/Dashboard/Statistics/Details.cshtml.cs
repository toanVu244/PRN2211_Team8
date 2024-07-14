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
    public class DetailsModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public DetailsModel(JPOS.Model.Entities.JPOS_ProjectContext context)
        {
            _context = context;
        }

      public Request Request { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            else 
            {
                Request = request;
            }
            return Page();
        }
    }
}
