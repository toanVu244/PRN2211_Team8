using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol;

namespace JPOS.Controller.Pages
{
    public class ForgetPasswordPageModel : PageModel
    {
        private readonly IUserServices _userServices;
        public bool check {  get; set; }
        [BindProperty]
         public string Email { get; set; } = default!;
        [BindProperty]
        public string OTP {  get; set; }
        [BindProperty]
        public string Password { get; set; }
        public ForgetPasswordPageModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        User User { get; set; } = default!;

        public void OnGet()
        {

        }



        public async Task<IActionResult> OnPostAsync()
        {
            check = await _userServices.ConfirmEmail(Email);  

            return Page();
        }

        public async Task<IActionResult> OnPostComfirmPassword()
        {

            await _userServices.ResetPassword(Email, Password, OTP);

            return Page();
        }
    }
}
