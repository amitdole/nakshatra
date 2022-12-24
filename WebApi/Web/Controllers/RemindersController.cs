using Api.Entities.Reminders;
using API.Model.Caching;
using Microsoft.AspNetCore.Mvc;
using Services.CacheService;
using Services.Extensions;
using Services.Services;

namespace EFCoreCosmosSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderService _reminderService;
        private readonly IConfiguration _config;


        public RemindersController(IReminderService reminderService, IConfiguration config)
        {
            _reminderService = reminderService;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var reminders = _reminderService.ListAllRemindersAsync();
            return Ok(reminders);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Reminder reminder)
        {
            await _reminderService.AddReminderAsync(reminder);
            return Ok(reminder);
        }

        [HttpDelete, Route("{reminerId}")]
        public async Task<IActionResult> Delete([FromRoute] string reminerId)
        {
            return Ok();
        }
    }
}
