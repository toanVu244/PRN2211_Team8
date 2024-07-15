using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.Dashboard.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IRequestService _requestService;
        private readonly IUserServices _userService;
        private readonly IProductService _productService;
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IRequestService requestService, IUserServices userService, IProductService productService, IHttpClientFactory httpClientFactory)
        {
            _requestService = requestService;
            _userService = userService;
            _productService = productService;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Request Request { get; set; } = new Request();

        public SelectList Users { get; set; }
        public SelectList Products { get; set; }
        public SelectList Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                var products = await _productService.GetAllProduct();

                Users = new SelectList(users, "UserId", "FullName");
                Products = new SelectList(products, "ProductId", "ProductName");
                Statuses = new SelectList(new List<string> { "Pending", "Processing", "Finished", "CheckedOut"});

                return Page();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load data. Please try again later.");
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            bool hasErrors = false;

            if (string.IsNullOrEmpty(Request.Image))
            {
                ModelState.AddModelError("Request.Image", "Image is required.");
                hasErrors = true;
            }
            else
            {
                bool imageUrlExists = await CheckImageUrlExists(Request.Image);
                if (!imageUrlExists)
                {
                    ModelState.AddModelError("Request.Image", "Image URL does not exist.");
                    hasErrors = true;
                }
            }

            if (!Request.Type.HasValue)
            {
                ModelState.AddModelError("Request.Type", "Type is required.");
                hasErrors = true;
            }

            if (hasErrors)
            {
                await LoadSelectListsAsync();
                return Page();
            }

            try
            {
                Request.CreateDate = DateTime.Now;
                var result = await _requestService.CreateRequestAsync(Request);

                if (result)
                {
                    return RedirectToPage("./Index");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save the request. Please try again.");
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "A database error occurred. Please try again later.");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An unexpected error occurred. Please try again later.");
            }

            await LoadSelectListsAsync();
            return Page();
        }

        private async Task<bool> CheckImageUrlExists(string imageUrl)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(imageUrl);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async Task LoadSelectListsAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            var products = await _productService.GetAllProduct();

            Users = new SelectList(users, "UserId", "FullName");
            Products = new SelectList(products, "ProductId", "ProductName");
            Statuses = new SelectList(new List<string> { "Pending", "Completed" });
        }
    }
}
