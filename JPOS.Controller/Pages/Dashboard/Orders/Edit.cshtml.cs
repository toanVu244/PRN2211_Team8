using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.Dashboard.Orders
{
    public class EditModel : PageModel
    {
        private readonly IRequestService _requestService;
        private readonly IUserServices _userService;
        private readonly IProductService _productService;

        public EditModel(IRequestService requestService, IUserServices userService, IProductService productService)
        {
            _requestService = requestService;
            _userService = userService;
            _productService = productService;
        }

        [BindProperty]
        public Request Request { get; set; } = default!;

        public string UserFullName { get; set; }

        public SelectList Products { get; set; }
        public SelectList Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Request = await _requestService.GetRequestByIDAsync(id.Value);
            await _requestService.DeatachRequest(Request);

            if (Request == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(Request.UserId);
            UserFullName = user.FullName;

            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await _requestService.UpdateRequestAsync(Request);

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

        private async Task LoadSelectListsAsync()
        {
            var products = await _productService.GetAllProduct();

            Products = new SelectList(products, "ProductId", "ProductName");
            Statuses = new SelectList(new List<string> { "Pending", "Processing", "Finished", "CheckedOut" });
        }
    }
}
