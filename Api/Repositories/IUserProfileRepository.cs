using Nakshatra.Api.Model.Profile;

namespace Nakshatra.Api.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfileInfo GetUserProfile(int profileId);
    }
}
