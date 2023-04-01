using Nakshatra.HostedServices.WebApi.Api.Entities.Profile;
using Nakshatra.Core.Services.Caching;
using Nakshatra.HostedServices.Services.Services;

namespace Nakshatra.HostedServices.WebApi.Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;
    private readonly ICacheService _cacheService;
    private const string userProfilesCacheKey = $"user_profile";
    private const string userProfileCacheKey = "user_profile_{0}";

    public ProfileController(IProfileService profileService, ICacheService cacheService)
    {
        _profileService = profileService;
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> List()
    {
        if (!_cacheService.TryGet(userProfilesCacheKey, out IReadOnlyList<Profile> profiles))
        {
            profiles = await _profileService.ListAllProfiles();
            _cacheService.Set(userProfilesCacheKey, profiles);
        }

        return Ok(profiles);
    }

    [HttpGet, Route("{profileId}")]
    public async Task<IActionResult> Get([FromRoute] int profileId)
    {
        if (!_cacheService.TryGet(string.Format(userProfileCacheKey, profileId), out Profile profile))
        {
            profile = await _profileService.GetProfile(profileId);
            _cacheService.Set(string.Format(userProfileCacheKey, profileId), profile);
        }

        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Profile profile)
    {
        var savedProfile = await _profileService.AddProfile(profile);
        _cacheService.Set(string.Format(userProfileCacheKey, profile.Id), profile);
        return Ok(savedProfile);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] Profile profile)
    {
        await _profileService.UpdateProfile(profile);

        _cacheService.Set(string.Format(userProfileCacheKey, profile.Id), profile);

        return Ok();
    }

    [HttpDelete, Route("{profileId}")]
    public async Task<IActionResult> Delete([FromRoute] int profileId)
    {
        await _profileService.DeleteProfile(profileId);

        return Ok();
    }
}
