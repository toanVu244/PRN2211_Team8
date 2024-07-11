using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.DesignPage
{
    public class CreateModel : PageModel
    {
        private readonly IDesignService _context;
        private readonly IProduct _product;
        public CreateModel(IDesignService contextpage)
        {
         _context = contextpage;
            }
    public IActionResult OnGet()
    {
        return Page();
    }

    [BindProperty]
    public Design Design { get; set; } = default!;


    // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
    public async Task<IActionResult> OnPostAsync()
    {
       /* if (!ModelState.IsValid || _context.Designs == null || Design == null)
        {
            return Page();
        }*/
            var a = await _context.CreateDesignAsync(Design, 1);

        return RedirectToPage("./Index");
    }
}

    internal interface IProduct
    {
    }
}

    

       