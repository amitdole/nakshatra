using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

namespace Nakshatra.HostedServices.WebApi.Services.Services;

public interface IProfileService
{
    Task<Profile> GetProfile(string profileId);
    Task<Profile> AddProfile(Profile profile);
    Task<IReadOnlyList<Profile>> ListAllProfiles();
    Task<bool> DeleteProfile(string profileId);
    Task UpdateProfile(Profile profile);
}
