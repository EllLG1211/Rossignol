using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class AuthController : RossignolControllerBase
    {
        [HttpGet]
        public IActionResult GetAuthToken(string email, string password)
        {
            return StatusCode(501);
        }
    }
}