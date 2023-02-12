using System.Reflection;
using AutoMapper;
using DTOs;
using EF_Local.Managers;
using Microsoft.AspNetCore.Mvc;
using Model.Business;
using Model.Business.Users;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class AccountsController : RossignolControllerBase
    {
        private IMapper _mapper;

        private readonly ILogger<AccountsController> _logger;

        private readonly IDataManager _data;

        public AccountsController(ILogger<AccountsController> logger, IMapper mapper, IDataManager data)
        {
            _logger = logger;
            _mapper = mapper;
            _data = data;
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
            var user = new ConnectedUser(account.Mail, account.Password);
            bool succeeded = _data.Register(user, user.Mail);
            if (!succeeded) return BadRequest();

            var createdResource = _data.GetUser(account.Mail);
            if (createdResource == null) return InternalSeverError();

            //var actionName = nameof(GetUserInfo);
            //return CreatedAtAction(actionName, ((MailedUser)createdResource).Mail);
            return NoContent();
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
