using JPOS.Model.Entities;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class IndexModel : PageModel
    {

        private readonly IProductService _repo;

        public IndexModel(IProductService _context)
        {
            _repo = _context;
        }

        public List<ProductModel> Product { get; set; } = default!;

        public async void OnGet()
        {
            if (_repo.GetAllProduct() != null)
            {
                Product = await _repo.GetAllProduct();
            }
        }
    }
}