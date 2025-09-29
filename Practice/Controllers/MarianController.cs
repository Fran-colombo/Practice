using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarianController : Controller
    {
        public IActionResult Get()
        {
            return Ok("Marian, te como todo el culo!!");
        }
    }
}
