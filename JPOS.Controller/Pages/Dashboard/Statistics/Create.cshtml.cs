using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Statistics
{
    public class CreateModel : PageModel
    {
        private readonly IRequestService _requestService;

        public CreateModel(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Request Request { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _requestService.GetAllRequestAsync ==null || Request == null)
            {
                return Page();
            }

            _requestService.CreateRequestAsync(Request);

            return RedirectToPage("./Index");
        }
    }
}
