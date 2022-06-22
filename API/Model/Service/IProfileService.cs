using API.Model.Profile;

namespace API.Model.Service
{
    public interface IProfileService
    {
        ProfileInfo GetProfile(int profileId);
    }
}