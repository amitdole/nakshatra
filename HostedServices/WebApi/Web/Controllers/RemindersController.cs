using Nakshatra.HostedServices.Services.Services;
using Nakshatra.HostedServices.WebApi.Api.Entities.Reminders;

namespace Nakshatra.HostedServices.WebApi.Web.Controllers;

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
        var reminders = await _reminderService.ListAllRemindersAsync();
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
        var reminders = await _reminderService.ListAllRemindersAsync();
        return Ok();
    }
}
