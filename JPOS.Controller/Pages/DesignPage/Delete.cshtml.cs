using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
namespace JPOS.Controller.Pages.DesignPage
{
    public class DeleteModel : PageModel
    {
        private readonly IDesignService _designService;

        public DeleteModel(IDesignService designService)
        {
            _designService = designService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _designService.GetAllDesignAsync == null)
            {
                return NotFound();
            }
            var design = await _designService.GetDesignByIdAsync(1);

            if (design != null)
            {
             
            }

            return RedirectToPage("./Index");
        }
    }
}
