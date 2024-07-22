using AutoMapper;
using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using JPOS.Service.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPOS.Controller.Pages.HomePages
{
    public class ProductCustomDetailModel : PageModel
    {
        private readonly IProductService productService;
        private readonly IMaterialService materialService;
        private readonly IProductMaterialService productMaterialService;
        private readonly IRequestService requestService;
        private readonly IMapper mapper;

        public ProductCustomDetailModel(IProductService productService, IMaterialService materialService, 
            IProductMaterialService productMaterialService, IRequestService requestService, IMapper mapper)
        {
            this.productService = productService;
            this.materialService = materialService;
            this.productMaterialService = productMaterialService;
            this.requestService = requestService;
            this.mapper = mapper;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public Category category { get; set; }

        [BindProperty]
        public List<MaterialShow> Materials { get; set; }

        public double ? Total { get; set; } 
        public int? TotalPrice { get; set; }

        public async Task<IActionResult> OnGet(int idProduct)
        {
            Product = await productService.GetProductByID(idProduct);
            Materials = mapper.Map<List<MaterialShow>>(await productMaterialService.GetmaterialByProductID(idProduct));
            Total = Product.ProcessPrice + Product.PriceMaterial + Product.PriceDesign;
            TotalPrice = Materials.Sum(m => m.Price);


            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            int productId = int.Parse(Request.Form["ProductId"]);            
            int newProductId = await productService.DuplicateProduct(productId);
            var newMaterial = await productMaterialService.GetmaterialByProductID(newProductId);
            var getPriceProduct = await productService.GetProductByIDTest(newProductId);
            await productService.DeatachProduct(getPriceProduct);
            for (int i = 0; ; i++)
            {
                var materialIdKey = $"Materials[{i}].MaterialId";
                var quantityKey = $"Materials[{i}].Quantity";
                var priceKey = $"Materials[{i}].Price";

                if (!Request.Form.ContainsKey(materialIdKey))
                    break;                
                double a = Math.Round(double.Parse(Request.Form[quantityKey]), 1, MidpointRounding.AwayFromZero) * 10;
                newMaterial[i].Quantity = Convert.ToInt32(a);             
            }
           

            foreach (var item in newMaterial)
            {
                //MaterialModel price = ListmateGetPrice.First(x => x.MaterialId == item.MaterialId);
                Material price = await materialService.GetmaterialByID(item.MaterialId);
                item.Price = item.Quantity * price.Price;
            }
            var finalPrice = newMaterial.Sum(m => m.Price);
            getPriceProduct.PriceMaterial = finalPrice;

            productMaterialService.UpdateMaterialProduct(newProductId, newMaterial);
            await productService.UpdateProductTest(getPriceProduct);
            string usID = HttpContext.Session.GetString("UserId");

            Request request = new Request()
            {
                CreateDate = DateTime.Now,
                Status = "Pending",
                ProductId = newProductId,
                Type = 2,
                UserId = usID
            };

            await requestService.CreateRequestAsync(request);
            TempData["TotalMoney"] = finalPrice + getPriceProduct.ProcessPrice + getPriceProduct.PriceDesign;
            TempData["RID"] = request.Id;

            return RedirectToPage("/HomePages/Checkout");
        }
    }

 
}
