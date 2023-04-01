using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    [Authorize]
    public class RemindersModel : PageModel
    {
        public RemindersModel()
        {
        }
        
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}