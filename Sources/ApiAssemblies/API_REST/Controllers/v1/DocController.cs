using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}")]
    public class DocController : ControllerBase
    {
        public IActionResult Get()
        {
            return StatusCode(501);
        }
    }
}
