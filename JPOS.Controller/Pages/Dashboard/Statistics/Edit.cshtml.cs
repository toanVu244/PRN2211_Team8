using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Statistics
{
    public class EditModel : PageModel
    {
        private readonly IRequestService _requestService;

        public EditModel(IRequestService requestService)
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
            Request = request;
/*           ViewData["ProductId"] = new SelectList(_requestService.Products, "ProductId", "ProductId");
           ViewData["UserId"] = new SelectList(_requestService.Users, "UserId", "UserId");*/
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _requestService.UpdateRequestAsync(Request);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;                
            }

            return RedirectToPage("./Index");
        }
    }
}
