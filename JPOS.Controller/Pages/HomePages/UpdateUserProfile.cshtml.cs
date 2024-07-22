using BusinessObject.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JPOS.Controller.Pages.HomePages
{
    public class UpdateUserProfileModel : PageModel
    {
        private readonly IUserServices _userService;

        public UpdateUserProfileModel(IUserServices userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        [BindProperty]
        public string CurrentPassword { get; set; }

        public SelectList Roles { get; set; }
        public SelectList Statuses { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _userService.GetUserByIdAsync(id);

            if (User == null)
            {
                return NotFound();
            }

            CurrentPassword = User.Password;

            await LoadRolesAndStatusesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userService.GetUserByIdAsync(User.UserId);

            if (currentUser == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(User.Password))
            {
                User.Password = currentUser.Password;
            }
            if (!string.IsNullOrEmpty(User.FullName))
            {
                currentUser.FullName = User.FullName;
            }

            if (!string.IsNullOrEmpty(User.PhoneNum))
            {
                currentUser.PhoneNum = User.PhoneNum;
            }

            if (!string.IsNullOrEmpty(User.Address))
            {
                currentUser.Address = User.Address;
            }

            if (!string.IsNullOrEmpty(User.Email))
            {
                currentUser.Email = User.Email;
            }
            currentUser.RoleId = User.RoleId;
            currentUser.Status = User.Status;
            var result = await _userService.UpdateUserAsync(currentUser);

            if (!result)
            {
                ModelState.AddModelError("", "Unable to save the user. Please try again.");
                await LoadRolesAndStatusesAsync();
                return Page();
            }

            return RedirectToPage("./Index");
        }


        private async Task LoadRolesAndStatusesAsync()
        {
            var roles = await _userService.GetAllRolesAsync();
            Roles = new SelectList(roles, "RoleId", "RoleName");

            var statusItems = new List<SelectListItem>
            {
                new SelectListItem { Value = "true", Text = "Active" },
                new SelectListItem { Value = "false", Text = "Inactive" }
            };

            Statuses = new SelectList(statusItems, "Value", "Text", User.Status.HasValue && User.Status.Value ? "true" : "false");
        }
    }
}
