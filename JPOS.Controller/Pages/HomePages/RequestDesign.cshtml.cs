using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages.HomePages
{
    public class RequestDesignModel : PageModel
    {
        private readonly ICategoryService categoryService;
        private readonly IRequestService requestService;

        public RequestDesignModel(ICategoryService categoryService,IRequestService requestService)
        {
            this.categoryService = categoryService;
            this.requestService = requestService;
        }
        public IList<Category> Category { get; set; } = default!;

        [BindProperty(Name = "requestText")]
        public string RequestText { get; set; }

        [BindProperty(Name = "selectedCategory")]
        public int SelectedCategoryId { get; set; }

        [BindProperty(Name = "imageUpload")]
        public IFormFile ImageUpload { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Category =await categoryService.GetAllCategoryAsync();
            return Page();

        }

        public async Task<IActionResult> OnPost()
        {
            
            Request request = new Request()
            {
                CreateDate = DateTime.Now,
                Description = "Create product type :"+ SelectedCategoryId +" request : "+ RequestText,
                Status = "Pending",
                Image = null,
                Type = 3,
                UserId = HttpContext.Session.GetString("UserID")
        };

         //  await requestService.CreateRequestAsync(request);
             
            return Page();
        }

        public void convertImage(IFormFile image)
        {

        }
    }
}
