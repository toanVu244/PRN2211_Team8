using JPOS.Model.Models;
using JPOS.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JPOS.Controller.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly IUserServices _repo;

        public RegisterPageModel(IUserServices _context)
        {
            _repo = _context;
        }

        public RegisterModel User { get; set; } = default!;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(ModelState.IsValid || User != null)
            {
                 return Page();
            }

            _repo.UserRegister(User);

            return RedirectToPage("LoginPage");
        }
    }
}
