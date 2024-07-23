using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using JPOS.Service.Implementations;
using static JPOS.Controller.Pages.Dashboard.Orders.IndexModel;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class IndexModel : PageModel
    {
        private readonly IDesignService _designService;

        public IndexModel(IDesignService designService)
        {
            _designService = designService;
        }

        [BindProperty]
        public IList<Design> Design { get; set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGetAsync()
        {
            Design = await _designService.GetAllDesignAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _designService.DeleteDesignAsync(id);
            return RedirectToPage();
        }

        public async Task OnPostSearchingDesign()
        {
            var designs = await _designService.GetAllDesignAsync();

            Design = designs.Select(c => new Design
            {
                DesignId = c.DesignId,
                Description = c.Description,
                CreateBy = c.CreateBy,
                Picture = c.Picture,
                CreateDate = c.CreateDate
            }).ToList();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Design = Design.Where(d => d.Description.ToLower().Contains(SearchTerm.ToLower())
                                        || d.CreateBy.ToLower().Contains(SearchTerm.ToLower())).ToList();
            }
        }

    }
}
