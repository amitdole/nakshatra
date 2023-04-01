using Nakshatra.HostedServices.Services.Repositories;
using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;

namespace Nakshatra.HostedServices.Services.Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _profileRepository;
    public ProfileService(IProfileRepository profileRepository) => _profileRepository = profileRepository;

    public async Task<Profile> AddProfile(Profile profile)
    {
        return await this._profileRepository.AddAsync(profile);
    }

    public async Task<IReadOnlyList<Profile>> ListAllProfiles()
    {
        return await _profileRepository.ListAllAsync();
    }

    public async Task<Profile> GetProfile(int profileId)
    {
        return await _profileRepository.GetByIdAsync(profileId);
    }

    public async Task UpdateProfile(Profile profile)
    {
        await _profileRepository.UpdateAsync(profile);
    }

    public async Task DeleteProfile(int profileId)
    {
        var profile = await _profileRepository.GetByIdAsync(profileId);
        await _profileRepository.DeleteAsync(profile);
    }
}
