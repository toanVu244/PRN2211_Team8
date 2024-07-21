using BusinessObject.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
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

        public ProductCustomDetailModel(IProductService productService, IMaterialService materialService, IProductMaterialService productMaterialService, IRequestService requestService)
        {
            this.productService = productService;
            this.materialService = materialService;
            this.productMaterialService = productMaterialService;
            this.requestService = requestService;
        }

        [BindProperty]
        public ProductModel Product { get; set; }

        public Category category { get; set; }

        [BindProperty]
        public List<ProductMaterial> Materials { get; set; }

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

        public async Task<IActionResult> OnPost()
        {
            int productId = int.Parse(Request.Form["ProductId"]);
            int newProductId = await productService.DuplicateProduct(productId);
            //Materials = new List<ProductMaterial>();
            var newMaterial = await productMaterialService.GetmaterialByProductID(newProductId);
            for (int i = 0; ; i++)
            {
                var materialIdKey = $"Materials[{i}].MaterialId";
                var quantityKey = $"Materials[{i}].Quantity";
                var priceKey = $"Materials[{i}].Price";

                if (!Request.Form.ContainsKey(materialIdKey))
                    break;

               /* var material = new ProductMaterial
                {
                    MaterialId = int.Parse(Request.Form[materialIdKey]),
                    Quantity = int.Parse(Request.Form[quantityKey]),
                    Price = int.Parse(Request.Form[priceKey])
                };*/
                newMaterial[i].Quantity = int.Parse(Request.Form[quantityKey]);             
            }
           

            foreach (var item in newMaterial)
            {
                //MaterialModel price = ListmateGetPrice.First(x => x.MaterialId == item.MaterialId);
                Material price = await materialService.GetmaterialByID(item.MaterialId);
                item.Price = item.Quantity * price.Price;
            }
            var finalPrice = newMaterial.Sum(m => m.Price);
            
            await productMaterialService.UpdateMaterialProduct(newProductId, newMaterial);
/*            var newProduct = await productService.GetProductByID(newProductId);
*/            string usID = HttpContext.Session.GetString("UserId");

            Request request = new Request()
            {
                CreateDate = DateTime.Now,
                Status = "Pending",
                ProductId = newProductId,
                Type = 1,
                UserId = usID
            };

            await requestService.CreateRequestAsync(request);
            TempData["TotalMoney"] = finalPrice;
            TempData["RID"] = request.Id;

            return RedirectToPage("/HomePages/Checkout");
        }
    }
}
