using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using JPOS.Service.Implementations;
using static System.Net.Mime.MediaTypeNames;

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

        [BindProperty]
        public List<string> SelectedMaterials { get; set; } = new List<string>();


        public async Task<IActionResult> OnGet()
        {

            ViewData["CategoryId"] = new SelectList(await categoryService.GetAllCategoryAsync(), "CatId", "CatName");
            ViewData["DesignId"] = new SelectList(await designService.GetAllDesignAsync(), "DesignId", "Picture");

            var materials = await materialService.GetAllMaterials();
            var materialList = materials.Select(m => new SelectListItem
            {
                Value = m.MaterialId.ToString(),
                Text = $"{m.Name} - ${m.Price} Per 0.1g" // Displaying name and price together
            });

            Material = new SelectList(materialList, "Value", "Text");

            return Page();
        }

       

        public async Task<IActionResult> OnPostAsync()
       {
            if (ImageFile == null || SelectedMaterials.Count < 1)
            {
                ModelState.AddModelError(string.Empty, "Fill all attribute before create.");
                return Page();
            }
            List<ProductMaterialModel> check = new List<ProductMaterialModel>();
            foreach (var material in SelectedMaterials)
            {
                var parts = material.Split(':');
                int materialId = int.Parse(parts[0]);
                double a = Math.Round(double.Parse(parts[1]), 1, MidpointRounding.AwayFromZero) *10;
                int quantity = Convert.ToInt32(a);
                ProductMaterialModel mate = new ProductMaterialModel()
                {
                    MaterialID = materialId,
                    Quantity = quantity,
                };
                check.Add(mate);    
            }
            string linkIMG = await ConvertImageToBase64AndUpload(ImageFile);
            Product.CreateBy = "Admin";
            Product.CreateDate = DateTime.Now;
            Product.Status = "Done";
            Product.Image = linkIMG;
            var ListmateGetPrice = await materialService.GetAllMaterials();
            foreach (var item in check)
            {
                MaterialModel price = ListmateGetPrice.First(x => x.MaterialId == item.MaterialID);
                item.Price = item.Quantity * price.Price;
            }
            Product.PriceMaterial = check.Sum(x => x.Price);
            await _productService.CreateProduct(Product, check);

            return RedirectToPage("./Index");
        }

       
        private async Task<string> ConvertImageToBase64AndUpload(IFormFile image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.CopyTo(ms);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return await _productService.UploadImageToCloudinary($"data:{image.ContentType};base64,{base64String}");
            }
        }
    }
}
