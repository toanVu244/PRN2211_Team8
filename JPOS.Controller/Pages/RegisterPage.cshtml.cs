using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly IUserServices _userService;

        public RegisterPageModel(IUserServices userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public RegisterModel User { get; set; } = default!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (User == null)
            {
                return Page();
            }

            _userService.UserRegister(User);

            return RedirectToPage("LoginPage");
        }
    }
}
