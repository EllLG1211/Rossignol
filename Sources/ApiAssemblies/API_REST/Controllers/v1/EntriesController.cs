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
        private const int MAX_AMOUNT = 30;

        private readonly ILogger<EntriesController> _logger;
        private IMapper _mapper;
        private readonly IDataManager data = new EFDataManager();


        public EntriesController(ILogger<EntriesController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a list of your entries
        /// </summary>
        /// <param name="user">Your user data</param>
        /// <param name="amount">How many entries you want. Enter a number between 1 and 30. Default is 10.</param>
        /// <param name="startingAt">The index you wish to start at.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List([FromBody] AccountDTO user, int amount = 10, int startingAt = 0)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{amount:{amount}, startingAt{startingAt}}}");
            if (amount > MAX_AMOUNT || amount < 1 || startingAt < 0) return StatusCode(403);

            var userModel = data.GetUser(user.Mail, user.Password);
            if (userModel == null) return Unauthorized();

            var entries = data.GetEntries(userModel).Skip(startingAt).Take(amount);
            if(!entries.Any()) return NotFound();
            return Ok(entries);
        }

        [HttpGet("/shared")]
        public IActionResult ListShared([FromBody] AccountDTO user, int amount = 10, int startingAt = 0)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{amount:{amount}, startingAt{startingAt}}}");
            if (amount > MAX_AMOUNT) return StatusCode(403);

            var userModel = data.GetUser(user.Mail, user.Password);
            if (userModel == null) return Unauthorized();

            if (!(userModel is ConnectedUser)) return InternalSeverError();

            var entries = data.GetSharedEntries((ConnectedUser)userModel).Skip(startingAt).Take(amount);
            if (!entries.Any()) return NotFound();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromBody] AccountDTO user)
        {
            Entry? entryModel = null;
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = data.GetUser(user.Mail, user.Password);
            if (userModel == null) return Unauthorized();

            entryModel = data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
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

        [HttpGet("/shared/{id}")]
        public IActionResult GetShared(string id, [FromBody] AccountDTO user)
        {
            Entry? entryModel = null;
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = data.GetUser(user.Mail, user.Password);
            if (userModel == null) return Unauthorized();

            if (!(userModel is ConnectedUser)) return InternalSeverError();
            entryModel = data.GetSharedEntries((ConnectedUser)userModel).First(e => e.Uid.ToString() == id);
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
        public IActionResult Add([FromBody] EntryDTO entry)
        {
            //log
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{entry:{entry}}}");

            //check the user is correct
            var userModel = data.GetUser(entry.Owner.Mail, entry.Owner.Password);
            if (userModel == null) return Unauthorized();

            //create entry and update user
            Entry entryModel = new ProprietaryEntry(entry.Owner.Mail, entry.Login, entry.Password, entry.App, entry.Note);
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
        public IActionResult Modify(string id, [FromBody] EntryDTO entry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}, entry: {entry}}}");

            var userModel = data.GetUser(entry.Owner.Mail, entry.Owner.Password);
            if (userModel == null) return Unauthorized();
            var oldEntry = data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (oldEntry == null) return NotFound();

            bool removalSucceeded = data.RemoveEntry(userModel, oldEntry);
            if (!removalSucceeded) return InternalSeverError();

            var newEntry = new ProprietaryEntry(entry.Owner.Mail, oldEntry.Uid, entry.Login, entry.Password, entry.App, entry.Note);
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