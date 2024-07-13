using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Implementations;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPOS.Controller.Pages.HomePages
{
    public class HomePageModel : PageModel
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public HomePageModel(IProductService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public IList<ProductModel> Product { get; set; } = default!;

        public IList<Category> Category { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public async Task OnGet()
        {
            Product = await productService.GetAllProduct();
            Category = await categoryService.GetAllCategoryAsync();
            Console.WriteLine(".....................................");
        }

        public async Task OnPost()
        {
            Product = await productService.GetAllProduct();
            Category = await categoryService.GetAllCategoryAsync();
            Console.WriteLine("nay vo dc r ne");
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Product = Product.Where(p => p.ProductName.Contains(SearchTerm)).ToList();
            }
        }

        public Task OnPostSearch()
        {
            Console.WriteLine("ahhhhh");

            return Task.CompletedTask;
           
        }
    }
}
