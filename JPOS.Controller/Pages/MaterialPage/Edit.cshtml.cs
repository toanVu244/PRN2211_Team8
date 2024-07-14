using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Model.Models;
using AutoMapper;

namespace JPOS.Controller.Pages.MaterialPage
{
    public class EditModel : PageModel
    {
        private readonly IMaterialService _service;
        private readonly IMapper mapper;

        public EditModel(IMaterialService _context, IMapper mapper)
        {
            _service = _context;
            this.mapper = mapper;
        }

        [BindProperty]
        public MaterialModel Material { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _service.GetAllmaterial == null)
            {
                return NotFound();
            }

            Material material = await _service.GetmaterialByID(id);
            if (material == null)
            {
                return NotFound();
            }

            Material = mapper.Map<MaterialModel>(material);

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

            try
            {
               await _service.UpdateMaterial(Material);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }

    }
}
