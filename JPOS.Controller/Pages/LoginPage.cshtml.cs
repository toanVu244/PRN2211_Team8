using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserServices _userService;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginPageModel(IUserServices userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userService.AuthenticateAsync(Email, Password);
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Role", user.RoleId.ToString());
                
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return Page();
        }
    }
}
