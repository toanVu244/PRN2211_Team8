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
        private readonly IProductService productService;

        public RequestDesignModel(ICategoryService categoryService,IRequestService requestService,IProductService productService)
        {
            this.categoryService = categoryService;
            this.requestService = requestService;
            this.productService = productService;
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
            string usID = HttpContext.Session.GetString("UserId");
            string imageUpload =await ConvertImageToBase64AndUpload(ImageUpload);

            TempData["TotalMoney"] = "50";
            TempData["UID"] = "US00000";
            TempData["Description"] = "Create product type :" + SelectedCategoryId + " request : " + RequestText;
            TempData["Status"] = "Pending";
            TempData["imageUpload"] = imageUpload;
            TempData["Type"] = "3";
            TempData.Keep();

            
            Category = await categoryService.GetAllCategoryAsync();

            return RedirectToPage("/HomePages/Checkout");
        }

        private async Task<string> ConvertImageToBase64AndUpload(IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return await productService.UploadImageToCloudinary($"data:{image.ContentType};base64,{base64String}");
            }
        }
    }
}
