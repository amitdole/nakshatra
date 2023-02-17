using Microsoft.AspNetCore.Mvc;

namespace Web_Api.Controllers
{
    public class TwitterController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] int i)
        {
            return  Ok();
        }
    }
}
