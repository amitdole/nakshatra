using Api.Entities.Profile;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace EFCoreCosmosSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService) => _profileService = profileService;

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var profiles = await this._profileService.ListAllProfiles();
            return Ok(profiles);
        }

        [HttpGet, Route("{profileId}")]
        public async Task<IActionResult> Get([FromRoute] string profileId)
        {
            var profile = await this._profileService.GetProfile(profileId);
            return Ok(profile);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Profile profile)
        {
            var savedProfile = await this._profileService.AddProfile(profile);

            return Ok(savedProfile);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Profile profile)
        {
            await this._profileService.UpdateProfile(profile);

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
