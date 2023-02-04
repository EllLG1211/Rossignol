using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class NotificationsController : RossignolControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult Put(string id, bool status)
        {
            return StatusCode(501);
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}