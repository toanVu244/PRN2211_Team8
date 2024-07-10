using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;

namespace JPOS.Controller.Pages.DesignPage
{
    public class CreateModel : PageModel
    {
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public CreateModel(JPOS.Model.Entities.JPOS_ProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Design Design { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Designs == null || Design == null)
            {
                return Page();
            }

            _context.Designs.Add(Design);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
