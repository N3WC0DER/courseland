using Microsoft.AspNetCore.Mvc;

namespace courseland.Server.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        // GET: UsersController
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Test");
        }
    }
}
