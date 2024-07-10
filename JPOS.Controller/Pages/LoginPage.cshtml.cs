using JPOS.Model.Entities;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class LoginPageModel : PageModel
    {
        private readonly IUserServices _memberRepo;

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public LoginPageModel(IUserServices _context)
        {
            _memberRepo = _context;
        }

        User user { get; set; } = default!;

        public void OnGet()
        {
        }

        public async void OnPost()
        {

            var Jwt = await _memberRepo.AuthenticateAsync(Email, Password);
            if (Jwt != null)
            {
                user = await _memberRepo.GetUserByEmail(Email);
                if(user != null) 
                {
                    HttpContext.Session.SetString("FullName", user.FullName);
                }
                HttpContext.Session.SetString("Email", Email);
                Response.Redirect("Index");
            }
        }

    }
}
