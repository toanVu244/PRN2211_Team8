using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages.HomePages
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IMaterialService materialService;
        private readonly IRequestService requestService;

        public ProductDetailModel(IProductService productService, IMaterialService materialService, IRequestService requestService)
        {
            this.productService = productService;
            this.materialService = materialService;
            this.requestService = requestService;
        }
        public ProductModel Product { get; set; }

        public IList<ProductModel> ListProduct { get; set; } = default!;
        public Category category { get; set; }
        public async Task<IActionResult> OnGet(int idProduct)
        {
            Product = await productService.GetProductByID(idProduct);
            TempData["TotalMoney"] = Product.ProcessPrice + Product.PriceMaterial + Product.PriceDesign;
            TempData["UID"] = "US00000";
            TempData["Description"] = Product.Description;
            TempData["Status"] = Product.Status;
            TempData["PID"] = Product.ProductId;
            TempData["Type"] = "1";
            TempData.Keep();
            ListProduct = await productService.GetAllProduct();
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string usID = HttpContext.Session.GetString("UserId");
            Request request = new Request()
            {
                CreateDate = DateTime.Now,
                Status = "Pending",
                Type = 1,
                UserId = usID
            };

            await requestService.CreateRequestAsync(request);
            TempData["TotalMoney"] = 10;
            TempData["RID"] = request.Id;

            return RedirectToPage("/HomePages/Checkout");
        }
    }
}
