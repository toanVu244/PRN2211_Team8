using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JPOS.Model.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Users
{
    public class EditModel : PageModel
    {
        private readonly IUserServices _userServices;
        private readonly JPOS.Model.Entities.JPOS_ProjectContext _context;

        public EditModel(IUserServices userServices, JPOS_ProjectContext context)
        {
            _userServices = userServices;
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _userServices.GetAllUsersAsync == null)
            {
                return NotFound();
            }

            var user =  await _userServices.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _userServices.UpdateUserAsync(User);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToPage("./Index");
        }
    }
}
