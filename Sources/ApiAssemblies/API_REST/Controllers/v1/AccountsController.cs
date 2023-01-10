using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccountEntity account)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] AccountEntity account)
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
