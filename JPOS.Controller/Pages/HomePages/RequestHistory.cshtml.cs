using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JPOS.Controller.Pages.HomePages
{
    public class RequestHistoryModel : PageModel
    {
        private readonly IRequestService _requestService;
        private readonly IProductService _productService;

        public RequestHistoryModel(IRequestService requestService, IProductService productService)
        {
            _requestService = requestService;
            _productService = productService;
        }

        public List<Request> Requests { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Login");
            }

            Requests = await _requestService.GetRequestsByUserIdAsync(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostCheckout(int id)
        {
            var request = await _requestService.GetRequestByIDAsync(id);
            if (request != null)
            {
                /*TempData["UID"] = request.UserId;
                TempData["Description"] = request.Description;
                TempData["Status"] = request.Status;
                TempData["PID"] = request.ProductId.ToString();
                TempData["Type"] = request.Type.ToString();
                TempData["TotalMoney"] = "100";  Replace with actual total money calculation*/
                var product = await _productService.GetProductByID(request.ProductId);
                if(product != null)
                {
                    TempData["TotalMoney"] = 10;
                    TempData["RID"] = request.Id;
                }

            }
            return RedirectToPage("/HomePages/Checkout");
        }
    }
}
