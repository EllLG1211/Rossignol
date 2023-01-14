using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class EntriesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult List(int page = 1)
        {
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Get(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Add([FromBody] EntryEntity entry)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Modify(string id, [FromBody] EntryEntity entry)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}