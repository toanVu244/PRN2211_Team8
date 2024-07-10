using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IList<ProductModel> Product { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_productService.GetAllProduct != null)
            {
                Product = await _productService.GetAllProduct();
            }
        }
    }
}