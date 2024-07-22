using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;

namespace JPOS.Controller.Pages.HomePages
{
    public class ViewUserProfileModel : PageModel
    {
        private readonly IUserServices _userServices;

        public ViewUserProfileModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public UserProfileModel User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            string usID = HttpContext.Session.GetString("UserId");
            if (usID == null || _userServices == null)
            {
                return NotFound();
            }

            var user = await _userServices.GetUserByIdAsync(usID);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                UserProfileModel model = new UserProfileModel
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    Email = user.Email,
                    Address = user.Address,
                    PhoneNum = user.PhoneNum
                };
                User = model;
            }
            return Page();
        }
    }
}
