using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Model.Models;
using AutoMapper;

namespace JPOS.Controller.Pages.Dashboard.Materials
{
    public class DeleteModel : PageModel
    {
        private readonly IMaterialService _service;
        private readonly IMapper mapper;

        public DeleteModel(IMaterialService _context, IMapper  mapper)
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
            else 
            {
                Material = mapper.Map<MaterialModel>(material);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _service.GetAllmaterial == null)
            {
                return NotFound();
            }

            var material = _service.GetmaterialByID(id);

            if (material != null)
            {
                Material.MaterialId = material.Id;
                await _service.DeleteMaterial(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
