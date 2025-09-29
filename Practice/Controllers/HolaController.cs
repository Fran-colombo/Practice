using Microsoft.AspNetCore.Mvc;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolaController : Controller
    {

        public IActionResult Get()
        {
            return Ok("Hola angi, te amo muchoo!");
        }
    }
}
