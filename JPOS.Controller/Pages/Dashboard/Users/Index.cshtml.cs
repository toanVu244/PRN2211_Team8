using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Service.Interfaces;
using JPOS.Model.Entities;
using JPOS.Service.Implementations;

namespace JPOS.Controller.Pages.Dashboard.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserServices _userService;

        public IndexModel(IUserServices userService)
        {
            _userService = userService;
        }

        public IList<UserViewModel> Users { get; set; }

        public async Task OnGetAsync()
        {
            var users = await _userService.GetAllUsersAsync();
            var roles = await _userService.GetAllRolesAsync();

            Users = users.Select(user => new UserViewModel
            {
                UserId = user.UserId,
                Username = user.Username,
                FullName = user.FullName,
                PhoneNum = user.PhoneNum,
                Address = user.Address,
                RoleName = roles.FirstOrDefault(role => role.RoleId == user.RoleId)?.RoleName,
                CreateDate = user.CreateDate ?? DateTime.MinValue, // Handle nullable DateTime
                Status = user.Status.HasValue && user.Status.Value ? "Active" : "Inactive", // Handle nullable bool
                Email = user.Email
            }).ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.DeleteUserAsync(userId);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Unable to delete the user. Please try again.");
            }

            return RedirectToPage();
        }

        public class UserViewModel
        {
            public string UserId { get; set; }
            public string Username { get; set; }
            public string FullName { get; set; }
            public string PhoneNum { get; set; }
            public string Address { get; set; }
            public string RoleName { get; set; }
            public DateTime CreateDate { get; set; }
            public string Status { get; set; }
            public string Email { get; set; }
        }
    }
}
