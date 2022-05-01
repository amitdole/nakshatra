using Api.Entities.Profile;
using Services.Repositories;

namespace Services.Services
{
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
            return await this._profileRepository.ListAllAsync();
        }

        public async Task<Profile> GetProfile(string profileId)
        {
            return await this._profileRepository.GetByIdAsync(profileId);
        }

        public async Task UpdateProfile(Profile profile)
        {
            await this._profileRepository.UpdateAsync(profile);
        }

        public async Task DeleteProfile(string profileId)
        {
            var profile = await this._profileRepository.GetByIdAsync(profileId);
            await this._profileRepository.DeleteAsync(profile);
        }
    }
}
