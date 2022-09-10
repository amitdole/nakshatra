using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Models;
using Services.Profile;
using API.Model.Service;
using API.Model.Profile;

namespace AspnetRun.Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;
        private IProfileService _profileService;
        private readonly Configuration _config;
      

        [BindProperty]
        public ProfileInfo Profile { get; set; }

        public ContactModel(ILogger<AboutModel> logger, IProfileService profileService, Configuration config)
        {
            _config = config;
            _logger = logger;
            _profileService = profileService;
        }

        public IActionResult OnGet()
        {
            var profile = _profileService.GetProfile(10001);

            Profile = profile;

            return Page();
        }

        public void OnPost(Contact contact)
        {
            var profile = _profileService.GetProfile(10001);

            var emailService = new EmailService(_config);
            var response = emailService.Send(new EmailInfo
            {
                Sender = contact.Email,
                Receiver = profile.PersonalDetails.Email,
                Subject = contact.Name,
                Message = contact.Message,

            });

            if (response != null && response.Result.IsSuccessStatusCode)
            {
                ViewData["Success"] = true;
            }

           
            Profile = profile;
        }
    }
}