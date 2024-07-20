using JPOS.Model.Entities;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class EditModel : PageModel
    {

        private readonly IDesignService _designService;

        public EditModel(IDesignService designService)
        {
            _designService = designService;
        }
        [BindProperty]
        public Design Design { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if(id == null || _designService.GetAllDesignAsync == null)
            {
                return NotFound();
            }
            var design = await _designService.GetDesignByIdAsync(id);
            if(design == null)
            {
                return NotFound();
            }
            Design = design;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                await _designService.UpdateDesignAsync(Design);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
