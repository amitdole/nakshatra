using Microsoft.Extensions.Configuration;
using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Repositories;
using Newtonsoft.Json;

namespace DataServices
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly ExtendedAttributes _configuration;
        private readonly IConfiguration _appSettings;
        public UserProfileRepository(ExtendedAttributes configuration, IConfiguration appSettings)
        {
            _configuration = configuration;
            _appSettings = appSettings;
        }

        UserProfileInfo IUserProfileRepository.GetUserProfile(int profileId)
        {
            var profile = new UserProfileInfo();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{_appSettings["SuryaWebApi:Endpoint"]}/api/profile/{_appSettings["Profile:Id"]}"))
                {
                    string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                    profile = JsonConvert.DeserializeObject<UserProfileInfo>(apiResponse);
                }
            }
            return profile;
        }
    }
};