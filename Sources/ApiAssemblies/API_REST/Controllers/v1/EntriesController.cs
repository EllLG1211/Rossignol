using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class EntriesController : ControllerBase
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
        public IActionResult Post([FromBody] EntryEntity entry)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] EntryEntity entry)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            return StatusCode(501);
        }
    }
}