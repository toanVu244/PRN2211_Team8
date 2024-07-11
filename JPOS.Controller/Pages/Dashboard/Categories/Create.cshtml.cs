using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ICategoryService _categoryService;

        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [BindProperty]
        public Category Category { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _categoryService.CreateCategoryAsync(Category);
            if (result)
            {
                return new JsonResult(new { success = true });
            }

            return BadRequest("Error creating category");
        }
    }

}
