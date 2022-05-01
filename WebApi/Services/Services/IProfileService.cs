using Api.Entities.Profile;

namespace Services.Services
{
    public interface IProfileService
    {
        Task<Profile> GetProfile(string familyId);
        Task<Profile> AddProfile(Profile profile);
        Task<IReadOnlyList<Profile>> ListAllProfiles();
        Task DeleteProfile(string profileId);
        Task UpdateProfile(Profile profile);
    }
}
