using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Materials
{
    public class CreateModel : PageModel
    {
        private readonly IMaterialService _materialService;

        public CreateModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        [BindProperty]
        public MaterialModel Material { get; set; } = new MaterialModel();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _materialService.CreateMaterial(Material);

            if (result == true)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to save the material. Please try again.");
                return Page();
            }
        }
    }
}
