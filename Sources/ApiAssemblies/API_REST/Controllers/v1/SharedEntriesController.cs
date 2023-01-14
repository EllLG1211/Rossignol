using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class SharedEntriesController : ControllerBase
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
        public IActionResult Add([FromBody] SharedEntryEntity sharedEntry)
        {
            return StatusCode(501);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status501NotImplemented)]
        public IActionResult Delete([FromBody] SharedEntryEntity sharedEntry)
        {
            return StatusCode(501);
        }
    }
}