using JPOS.Model.Entities;
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

        public ProductDetailModel(IProductService productService, IMaterialService materialService)
        {
            this.productService = productService;
            this.materialService = materialService;
        }
        public ProductModel Product { get; set; }

        public IList<ProductModel> ListProduct { get; set; } = default!;
        public Category category { get; set; }
        public async Task<IActionResult> OnGet(int idProduct)
        {
            Product = await productService.GetProductByID(idProduct);
            TempData["TotalMoney"] = Product.ProcessPrice + Product.PriceMaterial + Product.PriceDesign;
            TempData.Keep("TotalMoney");
            ListProduct = await productService.GetAllProduct();
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
