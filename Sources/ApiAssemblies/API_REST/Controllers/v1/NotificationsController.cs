using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : ControllerBase
    {
        public IActionResult Get()
        {
            return StatusCode(501);
        }

        public IActionResult Put(string id/*, params */)
        {
            return StatusCode(501);
        }

        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}