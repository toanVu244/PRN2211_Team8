using BusinessObject.Entities;
using JPOS.Repository.Repositories.Interfaces;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace JPOS.Controller.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserServices _userServices;

        public LoginPageModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, ErrorMessage = "Name cannot be longer than 16 characters")]
        public string Username { get; set; } = null;
        [BindProperty]
        [Required(ErrorMessage = "Pass is required")]
        [StringLength(16, ErrorMessage = "Name cannot be longer than 16 characters")]
        public string Password { get; set; } = null;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userServices.AuthenticateAsync(Username, Password);
            if (user != null)
            {
                if(user.Status == false)
                {
                    return RedirectToPage("AccessDeniedPage");
                }
                HttpContext.Session.SetString("UserName", user.Username.ToString());
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Role", user.RoleId.ToString());
                HttpContext.Session.SetString("Email", user.Email.ToString());
                HttpContext.Session.SetString("Address", user.Address);
                HttpContext.Session.SetString("PhoneNum", user.PhoneNum);

                return RedirectToPage("/HomePages/HomePage");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
