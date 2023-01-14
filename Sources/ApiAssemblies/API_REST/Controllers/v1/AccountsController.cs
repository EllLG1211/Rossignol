using API_REST.Services;
using ApiEntities;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {
        private IAccountServices services;

        [HttpGet("{id}")]
        public IActionResult GetUserInfo(string id)
        {
            var user = services.GetUser(id);
            if (user == null) return NotFound();
            else return Ok(user);
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
