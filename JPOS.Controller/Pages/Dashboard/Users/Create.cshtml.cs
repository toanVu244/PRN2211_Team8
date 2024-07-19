using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusinessObject.Entities;
using JPOS.Service.Interfaces;

namespace JPOS.Controller.Pages.Dashboard.Users
{
    public class CreateModel : PageModel
    {
        private readonly IUserServices _userService;

        public CreateModel(IUserServices userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; }

        public SelectList Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            Roles = new SelectList(roles, "RoleId", "RoleName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    var roles = await _userService.GetAllRolesAsync();
            //    Roles = new SelectList(roles, "RoleId", "RoleName");
            //    return Page();
            //}

            User.CreateDate = DateTime.Now;

            var result = await _userService.CreateUserAsync(User);

            if (result)
            {
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", "Unable to create user. Please try again.");
            return Page();
        }
    }
}
