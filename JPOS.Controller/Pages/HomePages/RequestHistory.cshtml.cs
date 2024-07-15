using JPOS.Model.Entities;
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

        public RequestHistoryModel(IRequestService requestService)
        {
            _requestService = requestService;
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
    }
}
