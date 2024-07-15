using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Materials
{
    public class IndexModel : PageModel
    {
        private readonly IMaterialService _materialService;

        public IndexModel(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        public IList<Material> Material { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_materialService != null)
            {
                Material = await _materialService.GetAllmaterial();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _materialService.DeleteMaterial(id);

            return RedirectToPage();
        }
    }
}
