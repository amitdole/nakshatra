namespace Nakshatra.HostedServices.WebApi.Web.Controllers
{
    public class TwitterController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] int i)
        {
            return  Ok();
        }
    }
}
