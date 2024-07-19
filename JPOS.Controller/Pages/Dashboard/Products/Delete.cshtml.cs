using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Products
{
    public class DeleteModel : PageModel
    {
        private readonly IProductService _productService;

        public DeleteModel(IProductService productService)
        {
            _productService = productService;
        }

        [BindProperty]
      public ProductModel Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _productService.GetAllProduct == null)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByID(id);

            if (product == null)
            {
                return NotFound();
            }
            else 
            {
                Product = product;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _productService.GetAllProduct ==null)
            {
                return NotFound();
            }
            var product = await _productService.GetProductByID(id);

            if (product != null)
            {
                
        
            }

            return RedirectToPage("./Index");
        }
    }
}
