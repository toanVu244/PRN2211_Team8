using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductViewModel> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_productService != null)
            {
                var products = await _productService.GetAllProduct();
                Product = products.Select(p => new ProductViewModel
                {
                    ProductId = p.ProductId,
                    CreateBy = p.CreateBy,
                    PriceMaterial = p.PriceMaterial,
                    PriceDesign = p.PriceDesign,
                    ProcessPrice = p.ProcessPrice,
                    CreateDate = p.CreateDate,
                    Status = p.Status,
                    Image = p.Image,
                    ProductName = p.ProductName,
                    Description = p.Description
                }).ToList();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (result)
            {
                return RedirectToPage();
            }
            else
            {
                // Handle deletion error
                ModelState.AddModelError(string.Empty, "Unable to delete the product. Please try again.");
                await OnGetAsync();
                return Page();
            }
        }

        public class ProductViewModel
        {
            public int ProductId { get; set; }
            public string CreateBy { get; set; }
            public int? PriceMaterial { get; set; }
            public int? PriceDesign { get; set; }
            public int? ProcessPrice { get; set; }
            public DateTime? CreateDate { get; set; }
            public string Status { get; set; }
            public string Image { get; set; }
            public string ProductName { get; set; }
            public string Description { get; set; }
        }
    }
}
