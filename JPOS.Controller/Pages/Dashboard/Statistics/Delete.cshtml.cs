using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Statistics
{
    public class DeleteModel : PageModel
    {
        private readonly IRequestService _requestService;

        public DeleteModel(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [BindProperty]
      public Request Request { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null || _requestService.GetAllRequestAsync == null)
            {
                return NotFound();
            }

            var request = await _requestService.GetRequestByIDAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            else 
            {
                Request = request;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null || _requestService.GetAllRequestAsync == null)
            {
                return NotFound();
            }
            var request = await _requestService.GetRequestByIDAsync(id);

            if (request != null)
            {
                Request = request;
                _requestService.DeleteRequestAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
