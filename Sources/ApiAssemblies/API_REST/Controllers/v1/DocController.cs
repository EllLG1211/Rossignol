using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class DocController : RossignolControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(501);
        }
    }
}
