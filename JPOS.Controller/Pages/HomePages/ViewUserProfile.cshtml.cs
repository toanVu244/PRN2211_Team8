using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using JPOS.Model.Entities;
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
            if (usID == null || _userServices.GetAllUsersAsync == null)
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
                UserProfileModel model = new UserProfileModel();
                model.FullName = user.FullName;
                model.Email = user.Email;
                model.Address = user.Address;
                model.PhoneNum = user.PhoneNum;
                User = model;
            }
            return Page();
        }
    }
}