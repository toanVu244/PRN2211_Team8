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
    public class DeleteModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public DeleteModel(JPOS.Model.Entities.JPOS_ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);

            if (request != null)
            {
                Request = request;
                _context.Requests.Remove(Request);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
