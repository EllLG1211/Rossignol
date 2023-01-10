using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {
        public IActionResult Get(string id)
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
