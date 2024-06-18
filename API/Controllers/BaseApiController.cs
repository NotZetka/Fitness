using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseApiController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("test");
        }
    }
}
