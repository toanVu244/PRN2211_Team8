using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Model.Models;
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

        public IList<ProductModel> Product { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_productService.GetAllProduct != null)
            {
                Product = await _productService.GetAllProduct();
            }
        }
    }
}
