using Microsoft.AspNetCore.Mvc;

namespace Spritify.Web.Controllers
{
    [ApiController]
    [Route("/")]
    public class UiController : ControllerBase
    {
        [HttpGet("{*url:nonfile}")]
        public IActionResult Index()
        {
            return File("~index.html", "text/html");
        }
    }
}
