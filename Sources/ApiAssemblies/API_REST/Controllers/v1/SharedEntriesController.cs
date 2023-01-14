using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class SharedEntriesController : ControllerBase
    {
        [HttpGet]
        public IActionResult List(int page = 1)
        {
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SharedEntryEntity sharedEntry)
        {
            return StatusCode(501);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SharedEntryEntity sharedEntry)
        {
            return StatusCode(501);
        }
    }
}