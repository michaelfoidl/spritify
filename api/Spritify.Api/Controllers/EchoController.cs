using Microsoft.AspNetCore.Mvc;

namespace Spritify.Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class EchoController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index(string message)
        {
            return Ok("Echo: " + message);
        }
    }
}
