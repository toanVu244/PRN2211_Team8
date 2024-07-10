using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;

namespace JPOS.Controller.Pages.DesignPage
{
    public class EditModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public EditModel(JPOS.Model.Entities.JPOS_ProjectContext context)
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

            var design =  await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == id);
            if (design == null)
            {
                return NotFound();
            }
            Design = design;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Design).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DesignExists(Design.DesignId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DesignExists(int id)
        {
          return (_context.Designs?.Any(e => e.DesignId == id)).GetValueOrDefault();
        }
    }
}
