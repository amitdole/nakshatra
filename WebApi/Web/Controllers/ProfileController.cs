using Api.Entities.Profile;
using API.Model.Caching;
using Microsoft.AspNetCore.Mvc;
using Services.CacheService;
using Services.Extensions;
using Services.Services;

namespace EFCoreCosmosSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly Func<CacheType, ICacheService> _cacheService;
        private readonly CacheType _cacheProvider;
        private readonly IConfiguration _config;
        private const string userProfilesCacheKey = $"user_profile";
        private const string userProfileCacheKey = "user_profile_{0}";

        public ProfileController(IProfileService profileService, Func<CacheType, ICacheService> cacheService, IConfiguration config)
        {
            _profileService = profileService;
            _cacheService = cacheService;
            _config = config;

            _cacheProvider = _config["CacheProvider"].ToEnum<CacheType>();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {


            if (!_cacheService(_cacheProvider).TryGet(userProfilesCacheKey, out IReadOnlyList<Profile> profiles))
            {
                profiles = await this._profileService.ListAllProfiles();
                _cacheService(_cacheProvider).Set(userProfilesCacheKey, profiles);
            }

            return Ok(profiles);
        }

        [HttpGet, Route("{profileId}")]
        public async Task<IActionResult> Get([FromRoute] string profileId)
        {
            if (!_cacheService(_cacheProvider).TryGet(string.Format(userProfileCacheKey, profileId), out Profile profile))
            {
                profile = await this._profileService.GetProfile(profileId);
                _cacheService(_cacheProvider).Set(string.Format(userProfileCacheKey, profileId), profile);
            }

            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Profile profile)
        {
            var savedProfile = await this._profileService.AddProfile(profile);
            _cacheService(_cacheProvider).Set(string.Format(userProfileCacheKey, profile.Id), profile);
            return Ok(savedProfile);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Profile profile)
        {
            await this._profileService.UpdateProfile(profile);

            _cacheService(_cacheProvider).Set(string.Format(userProfileCacheKey, profile.Id), profile);

            return Ok();
        }

        [HttpDelete, Route("{profileId}")]
        public async Task<IActionResult> Delete([FromRoute] string profileId)
        {
            await this._profileService.DeleteProfile(profileId);

            return Ok();
        }
    }
}
