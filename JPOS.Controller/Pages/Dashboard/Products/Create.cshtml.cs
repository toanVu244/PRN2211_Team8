using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
        /*ViewData["CategoryId"] = new SelectList(_context.Categories, "CatId", "CatId");
        ViewData["DesignId"] = new SelectList(_context.Designs, "DesignId", "DesignId");*/
            return Page();
        }

        [BindProperty]
        public ProductModel Product { get; set; } = default!;
        [BindProperty]
        public List<ProductMaterialModel> Materials { get; set; } = default;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _productService.GetAllProduct == null || Product == null)
            {
                return Page();
            }

            bool result = await _productService.CreateProduct(Product,Materials);

            return RedirectToPage("./Index");
        }
    }
}
