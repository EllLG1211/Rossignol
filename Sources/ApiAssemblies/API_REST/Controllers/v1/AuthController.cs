using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(string email, string password)
        {
            return StatusCode(501);
        }
    }
}