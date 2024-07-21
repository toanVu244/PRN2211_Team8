using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.DAO.EntitiesDAO;

namespace JPOS.Controller.Pages.DesignPage
{
    public class DetailsModel : PageModel
    {
        private readonly JPOS.DAO.EntitiesDAO.JPOS_ProjectContext _context;

        public DetailsModel(JPOS.DAO.EntitiesDAO.JPOS_ProjectContext context)
        {
            _context = context;
        }

      public Design Design { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Designs == null)
            {
                return NotFound();
            }

            var design = await _context.Designs.FirstOrDefaultAsync(m => m.DesignId == id);
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
