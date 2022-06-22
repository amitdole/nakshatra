using API.Repositories;
using API.Model.Service;
using API.Model.Profile;

namespace Services.Profile
{
    public class ProfileService : IProfileService
    {
        private IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            if (profileRepository != null)
                _profileRepository = profileRepository;
        }
        public ProfileInfo GetProfile(int profileId)
        {
            return _profileRepository.GetProfile(profileId);
        }
    }
}
