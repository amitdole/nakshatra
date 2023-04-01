using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;
using System.Configuration;
using Web.Models;

namespace AspnetRun.Web.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ILogger<AboutModel> _logger;
        private IUserProfileService _userProfileService;
        private readonly IConfiguration _config;
      

        [BindProperty]
        public UserProfileInfo Profile { get; set; }

        public ContactModel(ILogger<AboutModel> logger, IUserProfileService userProfileService, IConfiguration config)
        {
            _config = config;
            _logger = logger;
            _userProfileService = userProfileService;
        }

        public IActionResult OnGet()
        {
            var profile = _userProfileService.GetUserProfile(10001);

            Profile = profile;

            return Page();
        }

        public void OnPost(Contact contact)
        {
            var profile = _userProfileService.GetUserProfile(10001);

            var emailService = new Nakshatra.Services.Profile.EmailService(_config);
            var response = emailService.Send(new Nakshatra.Core.Api.Model.Email.EmailInfo
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