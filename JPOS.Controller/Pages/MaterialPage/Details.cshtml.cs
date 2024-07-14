using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using JPOS.Service.Implementations;
using JPOS.Model.Models;
using AutoMapper;

namespace JPOS.Controller.Pages.MaterialPage
{
    public class DetailsModel : PageModel
    {
        private readonly IMaterialService _service;
        private readonly IMapper mapper;

        public DetailsModel(IMaterialService _context,IMapper mapper)
        {
            _service = _context;
            this.mapper = mapper;
        }
        [BindProperty]
      public MaterialModel Material { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null && _service.GetmaterialByID(id) == null)
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
    }
}
