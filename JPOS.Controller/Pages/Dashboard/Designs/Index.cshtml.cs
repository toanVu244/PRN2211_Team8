using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class IndexModel : PageModel
    {
        private readonly IDesignService _designService;

        public IndexModel(IDesignService designService)
        {
            _designService = designService;
        }

        public IList<Design> Designs { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Designs = await _designService.GetAllDesignAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _designService.DeleteDesignAsync(id);
            return RedirectToPage();
        }
    }
}
