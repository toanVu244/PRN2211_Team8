using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using JPOS.Model.Models;

namespace JPOS.Controller.Pages.HomePages
{
    public class ViewUserProfileModel : PageModel
    {
        public UserProfileModel UserProfile { get; set; }

        public void OnGet()
        {
            // For demonstration, creating a sample user profile.
            UserProfile = new UserProfileModel
            {
                FullName = "John Doe",
                Email = "johndoe@example.com",
                PhoneNum = "123-456-7890",
                Address = "123 Main St, Anytown, USA"
            };
        }
    }
}