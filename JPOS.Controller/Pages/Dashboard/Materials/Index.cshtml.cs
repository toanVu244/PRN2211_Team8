using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Models;
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

        public IList<MaterialModel> Materials { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_materialService != null)
            {
                Materials = await _materialService.GetAllMaterials();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _materialService.DeleteMaterial(id);

            return RedirectToPage();
        }
    }
}
