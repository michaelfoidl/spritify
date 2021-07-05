using Microsoft.AspNetCore.Mvc;

namespace Spritify.Controllers
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
