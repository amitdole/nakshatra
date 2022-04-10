using API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IProfileService _profileService;

        [BindProperty]
        public API.Model.Profile Profile { get; set; }
        public IndexModel(ILogger<IndexModel> logger, IProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        public IActionResult OnGet()
        {
            var profile = _profileService.GetProfile(10001);

            Profile = profile;

            return Page();
        }

        public void OnPost(Web.Models.Contact profile)
        {
        }
    }
}