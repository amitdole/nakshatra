using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Model.Service;
using Nakshatra.Core.Services.Caching;
using Nakshatra.Core.Services.Email;
using Nakshatra.PersonalWebsite.Web.Models;

namespace Nakshatra.PersonalWebsite.Web.Pages
{
    public class ContactModel : PageModel
    {
        private IUserProfileService _userProfileService;
        private readonly IConfiguration _config;
        IEmailService _emailService;
        private readonly ICacheService _cacheService;
        private const string userProfileCacheKey = "user_profile_{0}";
        private readonly ILogger<ContactModel> _logger;


        [BindProperty]
        public UserProfileInfo Profile { get; set; }

        [BindProperty]
        public bool Success { get; set; }

        public ContactModel(ILogger<ContactModel> logger, IUserProfileService userProfileService, IConfiguration config, IEmailService emailService, ICacheService cacheService)
        {
            _config = config;
            _logger = logger;
            _userProfileService = userProfileService;
            _emailService = emailService;
            _cacheService = cacheService;
        }

        public IActionResult OnGet()
        {
            try
            {
                var profileId = int.Parse(_config["PersonalWebSiteUserId"]);
                var profileCacheKey = string.Format(userProfileCacheKey, profileId);

                if (!_cacheService.TryGet(profileCacheKey, out UserProfileInfo userProfile))
                {
                    userProfile = _userProfileService.GetUserProfile(profileId);
                    _cacheService.Set(profileCacheKey, userProfile);
                }

                Profile = userProfile;
            }
            catch (Exception e)
            {
                _logger.LogError("Error on Contact", e);
            }

            return Page();
        }

        public void OnPost(Contact contact)
        {
            try
            {
                if (contact.Email != null)
                {
                    var response = _emailService.Send(new Nakshatra.Core.Api.Model.Email.EmailInfo
                    {
                        SenderName = contact.Name,
                        SenderEmail = Profile.PersonalDetails.Email,
                        ReceiverName = $"{Profile.PersonalDetails.FirstName} {Profile.PersonalDetails.LastName}",
                        ReceiverEmail = Profile.PersonalDetails.ReceiveEmail,
                        Subject = contact.Subject,
                        Message = contact.Message
                    });

                    if (response != null && response.Result.IsSuccessStatusCode)
                    {
                        Success = true;
                    }
                    else
                    {
                        Success = false;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error on Contact", e);
            }
        }
    }
}