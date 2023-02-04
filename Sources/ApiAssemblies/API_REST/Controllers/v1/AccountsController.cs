using System.Reflection;
using API_REST.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class AccountsController : RossignolControllerBase
    {
        private IMapper _mapper;

        private ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetUserInfo(string id)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input: {{id: {id}}}");
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] AccountDTO account)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{account:{account}}}");
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult ChangeUserInfo(string id, [FromBody] AccountDTO account)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}, {{account:{account}}}"); // attention à ne pas logger le mot de passe
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(string id)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");
            return StatusCode(501);
        }
    }
}
