using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public IndexModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IList<Category> Category { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_categoryService != null)
            {
                Category = await _categoryService.GetAllCategoryAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);

            return RedirectToPage();
        }
    }
}
