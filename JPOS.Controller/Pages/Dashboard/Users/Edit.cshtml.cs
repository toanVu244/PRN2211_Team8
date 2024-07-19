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
    public class EditModel : PageModel
    {
        private readonly IUserServices _userService;

        public EditModel(IUserServices userService)
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
            // Lấy người dùng hiện tại từ cơ sở dữ liệu
            var currentUser = await _userService.GetUserByIdAsync(User.UserId);

            if (currentUser == null)
            {
                return NotFound();
            }

            // Sử dụng lại mật khẩu hiện tại nếu mk trống
            if (string.IsNullOrEmpty(User.Password))
            {
                User.Password = currentUser.Password;
            }

            //Chỉ khi không để trống mới cập nhật
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

            // Cập nhật các trường cần thiết, bao gồm cả Status
            //currentUser.FullName = User.FullName;
            //currentUser.PhoneNum = User.PhoneNum;
            //currentUser.Address = User.Address;
            currentUser.RoleId = User.RoleId;
            currentUser.Status = User.Status; 
            //currentUser.Email = User.Email;

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
