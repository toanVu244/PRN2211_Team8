using System.Collections.Generic;
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
            Category = await _categoryService.GetAllCategoryAsync();
        }

        public async Task<IActionResult> OnGetPartialAsync()
        {
            Category = await _categoryService.GetAllCategoryAsync();
            return Partial("_CategoriesPartial", Category);
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.CreateCategoryAsync(category);
            if (result)
            {
                return new JsonResult(new { success = true });
            }

            return BadRequest("Error creating category");
        }
    }
}
