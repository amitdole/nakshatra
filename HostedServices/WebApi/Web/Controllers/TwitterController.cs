namespace Nakshatra.HostedServices.WebApi.Web.Controllers
{
    public class TwitterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Add([FromBody] int i)
        {
            return  Ok();
        }
    }
}
