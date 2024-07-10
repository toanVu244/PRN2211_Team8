using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Model.Models;

namespace JPOS.Controller.Pages.MaterialPage
{
    public class CreateModel : PageModel
    {
        private readonly IMaterialService _service;

        public CreateModel(IMaterialService _context)
        {
            _service = _context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public MaterialModel Material { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (Material == null)
            {
                return Page();
            }

            Material.Status = "Available";
            await _service.CreateMaterial(Material); 

            return RedirectToPage("./Index");
        }
    }
}
