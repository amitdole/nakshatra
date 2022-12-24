using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using API.Model.Service;
using API.Model.Profile;

namespace AspnetRun.Web.Pages
{
    public class _ContactModel : PageModel
    {
        [BindProperty]
        public ProfileInfo Profile { get; set; }

        public _ContactModel()
        {
        }

        public IActionResult OnGet()
        {

            return Page();
        }

        public IActionResult OnGet(ProfileInfo profile)
        {
            Profile = profile;

            return Page();
        }
    }
}