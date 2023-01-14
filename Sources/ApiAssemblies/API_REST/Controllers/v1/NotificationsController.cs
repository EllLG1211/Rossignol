using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    //TODO fix this class
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Get()
        {
            return StatusCode(501);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Put(string id/*, params */) //TODO wtf is this method supposed to do idk
        {
            return StatusCode(501);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}