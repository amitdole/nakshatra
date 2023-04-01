using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

namespace Nakshatra.HostedServices.Services.Services;

public interface IProfileService
{
    Task<Profile> GetProfile(int profileId);
    Task<Profile> AddProfile(Profile profile);
    Task<IReadOnlyList<Profile>> ListAllProfiles();
    Task DeleteProfile(int profileId);
    Task UpdateProfile(Profile profile);
}
