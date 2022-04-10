using API.Model;
using API.Repositories;

namespace Services
{
    public class ProfileService : IProfileService
    {
        private IProfileRepository _profileRepository;
        public ProfileService(IProfileRepository profileRepository)
        {
            if (profileRepository != null)
                this._profileRepository = profileRepository;
        }
        public Profile GetProfile(int profileId)
        {
            return _profileRepository.GetProfile(profileId);
        }
    }
}
