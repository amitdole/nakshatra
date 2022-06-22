using Microsoft.Extensions.Configuration;
using API.Repositories;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using API.Model.Profile;

namespace DataServices
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Configuration _configuration;
        private readonly IConfiguration _appSettings;
        public ProfileRepository(Configuration configuration, Microsoft.Extensions.Configuration.IConfiguration appSettings)
        {
            _configuration = configuration;
            _appSettings = appSettings;
        }

        ProfileInfo IProfileRepository.GetProfile(int profileId)
        {
            var profile = new ProfileInfo();
            using (var httpClient = new HttpClient())
            {
                using (var response = httpClient.GetAsync($"{_appSettings["SuryaWebApi:Endpoint"]}/api/profile/{_appSettings["Profile:Id"]}"))
                {
                    string apiResponse = response.Result.Content.ReadAsStringAsync().Result;
                    profile = JsonConvert.DeserializeObject<ProfileInfo>(apiResponse);
                }
            }
            //}

            //    var data = _configuration.Metadata["UserProfiles"];
            //Profile profile = null;
            //Profile[]? profiles = JsonConvert.DeserializeObject<Profile[]>(data,
            //       new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });

            //if (profiles != null && profiles.Any())
            //{
            //    profile = profiles.Where(p => p.Id == profileId).FirstOrDefault();
            //}
            return profile;
        }
    }
};