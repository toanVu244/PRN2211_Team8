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
using Microsoft.EntityFrameworkCore;
using JPOS.Service.Implementations;

namespace JPOS.Controller.Pages.Dashboard.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService categoryService;
        private readonly IDesignService designService;
        private readonly IMaterialService materialService;

        public CreateModel(IProductService productService, ICategoryService categoryService, IDesignService designService,IMaterialService materialService)
        {
            _productService = productService;
            this.categoryService = categoryService;
            this.designService = designService;
            this.materialService = materialService;
        }

        [BindProperty]
        public ProductModel Product { get; set; } = default!;

        [BindProperty]
        public IFormFile ImageFile { get; set; }    
        
        public SelectList Material { get; set; }

        public async Task<IActionResult> OnGet()
        {

            ViewData["CategoryId"] = new SelectList(await categoryService.GetAllCategoryAsync(), "CatId", "CatName");
            ViewData["DesignId"] = new SelectList(await designService.GetAllDesignAsync(), "DesignId", "Picture");

            var materials = await materialService.GetAllmaterial();
            var materialList = materials.Select(m => new SelectListItem
            {
                Value = m.MaterialId.ToString(),
                Text = $"{m.Name} - ${m.Price}" // Displaying name and price together
            });

            Material = new SelectList(materialList, "Value", "Text");

            return Page();
        }

       

        public async Task<IActionResult> OnPostAsync()
       {       
            return RedirectToPage("./Index");
        }
    }
}
