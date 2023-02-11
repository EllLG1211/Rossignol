using System.Reflection;
using AutoMapper;
using DTOs;
using EF_Local.Managers;
using Microsoft.AspNetCore.Mvc;
using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class EntriesController : RossignolControllerBase
    {
        private readonly ILogger<EntriesController> _logger;
        private IMapper _mapper;
        private readonly IDataManager data = new EFDataManager();


        public EntriesController(ILogger<EntriesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult List(int page = 1)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{page:{page}}}");
            return StatusCode(501);
        }

        [HttpGet("/shared")]
        public IActionResult ListShared(int page = 1)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{page:{page}}}");
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromBody] AccountDTO user)
        {
            Entry? entryModel = null;
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = data.GetUser(user.Mail, user.Password);
            if (userModel == null) return Unauthorized();

            entryModel = data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (entryModel == null)
            {
                if (userModel is ConnectedUser connectedUserModel) 
                    entryModel = data.GetSharedEntries(connectedUserModel).First(e => e.Uid.ToString() == id);
                else return NotFound();
            }
            if (entryModel == null) return NotFound();

            EntryDTO entryDto = new()
            {
                Uid = entryModel.Uid,
                Login = entryModel.Login,
                Password = entryModel.Password,
                App = entryModel.App,
                Note = entryModel.App
            };

            return Ok(entryDto);
        }

        [HttpPost]
        public IActionResult Add([FromBody] EntryDTO entry, [FromBody] AccountDTO owner)
        {
            //log
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{entry:{entry}}}");

            //check the user is correct
            var userModel = data.GetUser(owner.Mail, owner.Password);
            if (userModel == null) return Unauthorized();

            //create entry and update user
            Entry entryModel = new ProprietaryEntry(owner.Mail, entry.Login, entry.Password, entry.App, entry.Note);
            var creationSucceeded = data.AddEntryToUser(userModel, entryModel);
            data.UpdateUser(userModel);
            if (!creationSucceeded) return BadRequest();

            //get created resource
            var createdResource = data.GetEntries(userModel).First(e => e == entryModel);
            if (createdResource == null) return InternalSeverError();

            //succeeded: return result
            var actionName = nameof(Get);
            return CreatedAtAction(actionName, createdResource.Uid, createdResource);
        }

        [HttpPut("{id}")]
        public IActionResult Modify(string id, [FromBody] EntryDTO entry, [FromBody] AccountDTO owner)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}, entry: {entry}}}");

            var userModel = data.GetUser(owner.Mail, owner.Password);
            if (userModel == null) return Unauthorized();
            var oldEntry = data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (oldEntry == null) return NotFound();

            bool removalSucceeded = data.RemoveEntry(userModel, oldEntry);
            if (!removalSucceeded) return InternalSeverError();

            var newEntry = new ProprietaryEntry(owner.Mail, oldEntry.Uid, entry.Login, entry.Password, entry.App, entry.Note);
            bool readdSucceeded = data.AddEntryToUser(userModel, newEntry);
            if (readdSucceeded) return NoContent();
            else return InternalSeverError();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] AccountDTO owner)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = data.GetUser(owner.Mail, owner.Password);
            if (userModel == null) return Unauthorized();
            var entryModel = data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (entryModel == null) return NotFound();

            bool removalSucceeded = data.RemoveEntry(userModel, entryModel);
            if (removalSucceeded) return NoContent();
            else return InternalSeverError();
        }
    }
}