using Nakshatra.Api.Model.Profile;

namespace Nakshatra.Api.Model.Service
{
    public interface IUserProfileService
    {
        UserProfileInfo GetUserProfile(int profileId);
    }
}