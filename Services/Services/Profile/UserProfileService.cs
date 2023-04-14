using Nakshatra.Api.Model.Profile;
using Nakshatra.Api.Repositories;
using Nakshatra.Api.Model.Service;

namespace Nakshatra.Services.Profile;

public class UserProfileService : IUserProfileService
{
    private IUserProfileRepository _userProfileRepository;
    public UserProfileService(IUserProfileRepository userprofileRepository)
    {
        if (userprofileRepository != null)
            _userProfileRepository = userprofileRepository;
    }
    public UserProfileInfo GetUserProfile(int profileId)
    {
        return _userProfileRepository.GetUserProfile(profileId);
    }
}
