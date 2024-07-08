using AutoMapper.Execution;
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

        public LoginPageModel(IUserServices memberRepo)
        {
            _memberRepo = memberRepo;
        }

        public void OnGet()
        {
        }

        public async void OnPost()
        {

            var Jwt = await _memberRepo.AuthenticateAsync(Email, Password);
            if (Jwt != null)
            {
                HttpContext.Session.SetString("Email", Email);
                Response.Redirect("Index");
            }
        }

    }
}

