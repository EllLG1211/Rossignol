using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult GetUserInfo(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AccountEntity account)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult ChangeUserInfo(string id, [FromBody] AccountEntity account)
        {
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            return StatusCode(501);
        }
    }
}
