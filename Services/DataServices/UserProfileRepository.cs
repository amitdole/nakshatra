using Microsoft.Extensions.Configuration;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Repositories;
using Newtonsoft.Json;

namespace Nakshatra.DataServices
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IConfiguration _appSettings;
        public UserProfileRepository(IConfiguration appSettings)
        {
            _appSettings = appSettings;
        }

        UserProfileInfo IUserProfileRepository.GetUserProfile(int profileId)
        {
            var profile = new UserProfileInfo();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{_appSettings["SuryaWebApi:Endpoint"]}/api/profile/{_appSettings["PersonalWebSiteUserId"]}"))
                {
                    string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                    profile = JsonConvert.DeserializeObject<UserProfileInfo>(apiResponse);
                }
            }
            return profile;
        }
    }
};