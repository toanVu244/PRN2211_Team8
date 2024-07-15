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

        public IList<UserViewModel> Users { get; set; } = new List<UserViewModel>(); // Khởi tạo thuộc tính Users

        public async Task<IActionResult> OnPostDeleteAsync(string userId)
        {
            if (userId == null)
            {
                return NotFound();
            }

            // Kiểm tra xem ID người dùng hiện tại trong session có đang được xóa hay không
            var currentUserId = HttpContext.Session.GetString("UserId");
            if (currentUserId == userId)
            {
                ModelState.AddModelError(string.Empty, "You cannot delete your own account.");
                await LoadUsersAsync(); // Tải lại danh sách người dùng
                return Page();
            }

            var result = await _userService.DeleteUserAsync(userId);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "User has related data. Please set the status to Inactive instead of deleting.");
                await LoadUsersAsync(); // Tải lại danh sách người dùng
                return Page();
            }

            return RedirectToPage();
        }

        private async Task LoadUsersAsync()
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
                CreateDate = user.CreateDate ?? DateTime.MinValue,
                Status = user.Status.HasValue && user.Status.Value ? "Active" : "Inactive",
                Email = user.Email
            }).ToList();
        }

        public async Task OnGetAsync()
        {
            await LoadUsersAsync();
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
