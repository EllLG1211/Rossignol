using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class EntriesController : ControllerBase
    {
        public IActionResult Get(int page = 1)
        {
            return StatusCode(501);
        }

        public IActionResult Post(/*params*/) //TODO add params
        {
            return StatusCode(501);
        }

        public IActionResult Put(string id/*, params */) //TODO add params
        {
            return StatusCode(501);
        }

        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}