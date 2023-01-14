using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    //TODO fix this class
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return StatusCode(501);
        }

        [HttpPut]
        public IActionResult Put(string id/*, params */) //TODO wtf is this method supposed to do idk
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