using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using JPOS.Service.Implementations;
using static JPOS.Controller.Pages.Dashboard.Products.IndexModel;
using JPOS.Model.Models;

namespace JPOS.Controller.Pages.Dashboard.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IRequestService _requestService;
        private readonly IUserServices _userService;
        private readonly IProductService _productService;

        public IndexModel(IRequestService requestService, IUserServices userService, IProductService productService)
        {
            _requestService = requestService;
            _userService = userService;
            _productService = productService;
        }

        public IList<RequestViewModel> Requests { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public async Task OnGetAsync()
        {
            if (_requestService != null)
            {
                var requests = await _requestService.GetAllRequestAsync();
                var users = await _userService.GetAllUsersAsync();
                var products = await _productService.GetAllProduct();

                Requests = requests.Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    User = users.FirstOrDefault(u => u.UserId == r.UserId)?.FullName,
                    Description = r.Description,
                    CreateDate = r.CreateDate,
                    Status = r.Status,
                    ProductName = products.FirstOrDefault(p => p.ProductId == r.ProductId)?.ProductName,
                    Image = r.Image,
                    Type = r.Type
                }).ToList();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await _requestService.DeleteRequestAsync(id);

            return RedirectToPage();
        }

        public class RequestViewModel
        {
            public int Id { get; set; }
            public string User { get; set; }
            public string Description { get; set; }
            public DateTime? CreateDate { get; set; }
            public string Status { get; set; }
            public string ProductName { get; set; }
            public string Image { get; set; }
            public int? Type { get; set; }
        }

        public async Task OnPostSearchingOrder()
        {
            var requests = await _requestService.GetAllRequestAsync();
            var users = await _userService.GetAllUsersAsync();
            var products = await _productService.GetAllProduct();

            Requests = requests.Select(r => new RequestViewModel
            {
                Id = r.Id,
                User = users.FirstOrDefault(u => u.UserId == r.UserId)?.FullName,
                Description = r.Description,
                CreateDate = r.CreateDate,
                Status = r.Status,
                ProductName = products.FirstOrDefault(p => p.ProductId == r.ProductId)?.ProductName,
                Image = r.Image,
                Type = r.Type
            }).ToList();
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                Requests = Requests.Where(p => p.ProductName.ToLower().Contains(SearchTerm.ToLower()) 
                                            || p.User.ToLower().Contains(SearchTerm.ToLower())).ToList();
            }
        }

    }

}
