using API_REST.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class AccountsController : RossignolControllerBase
    {
        [Inject]
        public IMapper Mapper { get; init; }

        [HttpGet("{id}")]
        public IActionResult GetUserInfo(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AccountDTO account)
        {
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult ChangeUserInfo(string id, [FromBody] AccountDTO account)
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
