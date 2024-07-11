using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.DesignPage
{
    public class DetailsModel : PageModel
    {
        private readonly IDesignService _designService;

        public DetailsModel(IDesignService designService)
        {
            _designService = designService;
        }
        
      public Design Design { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _designService.GetAllDesignAsync == null)
            {
                return NotFound();
            }

            var design = await _designService.GetDesignByIdAsync(1);
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
    }
}
