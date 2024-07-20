using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace JPOS.Controller.Pages.Dashboard.Designs
{
    public class CreateModel : PageModel
    {
        private readonly IDesignService _designService;

        public CreateModel(IDesignService designService)
        {
            _designService = designService;
        }

        [BindProperty]
        public Design Design { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            byte[] img =File().ReadAllBytes();
            Convert.ToBase64String(img);
            try
            {
                await _designService.CreateDesignAsync(Design,0);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
