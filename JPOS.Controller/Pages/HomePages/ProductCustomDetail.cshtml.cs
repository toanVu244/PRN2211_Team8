using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages.HomePages
{
    public class ProductCustomDetailModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IMaterialService materialService;
        private readonly IProductMaterialService productMaterialService;

        public ProductCustomDetailModel(IProductService productService, 
            IMaterialService materialService,IProductMaterialService productMaterialService)
        {
            this.productService = productService;
            this.materialService = materialService;
            this.productMaterialService = productMaterialService;
        }
        public ProductModel Product { get; set; }
        public Category category { get; set; }
        public IList<ProductMaterial> Materials { get; set; }
        public int? TotalPrice { get; set; }
        public async Task<IActionResult> OnGet(int idProduct)
        {
            Product = await productService.GetProductByID(idProduct);
            Materials = await productMaterialService.GetmaterialByProductID(idProduct);
            TotalPrice = Materials.Sum(m => m.Price);
            if (Product == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}

