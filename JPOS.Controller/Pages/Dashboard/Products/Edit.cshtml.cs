﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Products
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly ICategoryService categoryService;
        private readonly IDesignService designService;
        private readonly IProductMaterialService productMaterialService;

        public EditModel(IProductService productService,ICategoryService categoryService,
            IDesignService designService,IProductMaterialService productMaterialService)
        {
            _productService = productService;
            this.categoryService = categoryService;
            this.designService = designService;
            this.productMaterialService = productMaterialService;
        }

        [BindProperty]
        public ProductModel Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _productService.GetAllProduct ==null)
            {
                return NotFound();
            }
            var productmodel = await _productService.GetProductByID(id);
            var product = await _productService.GetProductByIDTest(id);
            await _productService.DeatachProduct(product);
            if (product == null)
            {
                return NotFound();
            }
            Product = productmodel;
            ViewData["CategoryId"] = new SelectList(await categoryService.GetAllCategoryAsync(), "CatId", "CatId");
            ViewData["DesignId"] = new SelectList(await designService.GetAllDesignAsync(), "DesignId", "DesignId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _productService.UpdateProduct(Product);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }

        
    }
}
