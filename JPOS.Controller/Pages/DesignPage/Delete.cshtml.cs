using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;

namespace JPOS.Controller.Pages.DesignPage
{
    public class DeleteModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public DeleteModel(JPOS.Model.Entities.JPOS_ProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Design Design { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Designs == null)
            {
                return NotFound();
            }

            var design = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == id);

            if (design == null)
            {
                return NotFound();
            }
            else 
            {
                Design = design;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Designs == null)
            {
                return NotFound();
            }
            var design = await _context.Designs.FindAsync(id);

            if (design != null)
            {
                Design = design;
                _context.Designs.Remove(Design);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
