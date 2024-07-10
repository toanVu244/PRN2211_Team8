using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class ForgetPasswordPageModel : PageModel
    {
        private readonly IUserServices _userServices;

        [BindProperty]
        string Email { get; set; }

        public ForgetPasswordPageModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        User User { get; set; } = default!;

        public void OnGet()
        {

        }

       /* public async Task<IActionResult> OnPostAsync() 
        {

            User = await _userServices.GetUserByEmail(Email);
            if(User != null)
            {
                _userServices.UpdateUserAsync(User);
            }
        }*/
    }
}
