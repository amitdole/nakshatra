using API.Model;
using Microsoft.Extensions.Configuration;
using API.Repositories;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace DataServices
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly Configuration _configuration;
        public ProfileRepository(Configuration configuration)
        {
            _configuration = configuration;
        }

        Profile IProfileRepository.GetProfile(int profileId)
        {
            var data = _configuration.Metadata["UserProfiles"];
            Profile profile = null;
            Profile[]? profiles = JsonConvert.DeserializeObject<Profile[]>(data,
                   new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" });

            if (profiles != null && profiles.Any())
            {
                profile = profiles.Where(p => p.Id == profileId).FirstOrDefault();
            }
            return profile;
        }
    }
};