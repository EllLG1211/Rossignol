using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using API_Gateway.Helpers;
using AutoMapper;
using DTOs;
using EF_Local.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Business;
using Model.Business.Entries;
using Model.Business.Users;

namespace API_REST.Controllers.V1
{
 //   [Authorize]
    [ApiController]
    public class EntriesController : RossignolControllerBase
    {
        private const int MAX_AMOUNT = 30;

        private readonly ILogger<EntriesController> _logger;
        private IMapper _mapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IDataManager _data;


        public EntriesController(ILogger<EntriesController> logger, IMapper mapper, IDataManager data, IJwtUtils jwtUtils)
        {
            _logger = logger;
            _mapper = mapper;
            _data = data;
            _jwtUtils = jwtUtils;
        }

        /// <summary>
        /// Get a list of your entries
        /// </summary>
        /// <param name="user">Your user data</param>
        /// <param name="amount">How many entries you want. Enter a number between 1 and 30. Default is 10.</param>
        /// <param name="startingAt">The index you wish to start at.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult List([FromHeader] string Authorization, int amount = 10, int startingAt = 0)
        {
            var email = _jwtUtils.ValidateJwtToken(Authorization);
            if (email == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, "Could not find email in bearer token");
                return Unauthorized();
            }

            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{amount:{amount}, startingAt{startingAt}}}");
            if (amount > MAX_AMOUNT || amount < 1 || startingAt < 0)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"Parameters were incorrect");
                return StatusCode(403);
            }

            var userModel = _data.GetUser(email);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {email} was not found");
                return Unauthorized();
            }

            var entries = _data.GetEntries(userModel).Skip(startingAt).Take(amount);
            if(!entries.Any()) return NotFound();
            return Ok(entries);
        }

        /// <summary>
        /// Get a list of entries that were shared with you
        /// </summary>
        /// <param name="user">Your user data</param>
        /// <param name="amount">How many entries you want. Max value is 30.</param>
        /// <param name="startingAt">The index you wish to start at.</param>
        /// <returns></returns>
        [HttpGet("shared")]
        public IActionResult ListShared([FromBody] AccountDTO user, int amount = 10, int startingAt = 0)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{amount:{amount}, startingAt{startingAt}}}");
            if (amount > MAX_AMOUNT) return StatusCode(403);

            var userModel = _data.GetUser(user.Mail, user.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {user.Mail} was not found");
                return Unauthorized();
            }

            if (userModel is not ConnectedUser)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, "DataManager did not return a ConnectedUser");
                return InternalSeverError();
            }

            var entries = _data.GetSharedEntries((ConnectedUser)userModel).Skip(startingAt).Take(amount);
            if (!entries.Any()) return NotFound();
            return Ok(entries);
        }

        /// <summary>
        /// Get a single of your entries
        /// </summary>
        /// <param name="id">The entry's UID</param>
        /// <param name="user">Your user data</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(string id, [FromBody] ConnectedUserDTO user)
        {
            Entry? entryModel = null;
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = _data.GetUser(user.Mail, user.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {user.Mail} was not found");
                return Unauthorized();
            }

            entryModel = _data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
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

        /// <summary>
        /// Get a single entry that was shared with you
        /// </summary>
        /// <param name="id">The entry's UID</param>
        /// <param name="user">Your user data</param>
        /// <returns></returns>
        [HttpGet("shared/{id}")]
        public IActionResult GetShared(string id, [FromBody] ConnectedUserDTO user)
        {
            Entry? entryModel = null;
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = _data.GetUser(user.Mail, user.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {user.Mail} was not found");
                return Unauthorized();
            }

            if (userModel is not ConnectedUser)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, "DataManager did not return a ConnectedUser");
                return InternalSeverError();
            }
            entryModel = _data.GetSharedEntries((ConnectedUser)userModel).First(e => e.Uid.ToString() == id);
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

        /// <summary>
        /// Create a new entry
        /// </summary>
        /// <param name="entry">Data for the entry</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add([FromBody] EntryDTO entry)
        {
            //log
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{entry:{entry}}}");

            //check the user is correct
            var userModel = _data.GetUser(entry.Owner.Mail, entry.Owner.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {entry.Owner.Mail} was not found");
                return Unauthorized();
            }

            //create entry and update user
            Entry entryModel = new ProprietaryEntry(entry.Owner.Mail, entry.Login, entry.Password, entry.App, entry.Note);
            var creationSucceeded = _data.AddEntryToUser(userModel, entryModel);
            _data.UpdateUser(userModel);
            if (!creationSucceeded)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"resource creation failed");
                return BadRequest();
            }

            //get created resource
            var createdResource = _data.GetEntries(userModel).First(e => e == entryModel);
            if (createdResource == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"AddEntryToUser returned true but resource could not be retrieved");
                return InternalSeverError();
            }

            //succeeded: return result
            var actionName = nameof(Get);
            return CreatedAtAction(actionName, createdResource.Uid, createdResource);
        }

        /// <summary>
        /// Update an existing entry
        /// </summary>
        /// <param name="id">UID of the entry</param>
        /// <param name="entry">Data for the updated entry</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Modify(string id, [FromBody] EntryDTO entry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}, entry: {entry}}}");

            var userModel = _data.GetUser(entry.Owner.Mail, entry.Owner.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {entry.Owner.Mail} was not found");
                return Unauthorized();
            }

            var oldEntry = _data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (oldEntry == null) return NotFound();

            bool removalSucceeded = _data.RemoveEntry(userModel, oldEntry);
            if (!removalSucceeded)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"entry was found in database but could not be removed");
                return InternalSeverError();
            }

            var newEntry = new ProprietaryEntry(entry.Owner.Mail, oldEntry.Uid, entry.Login, entry.Password, entry.App, entry.Note);
            bool readdSucceeded = _data.AddEntryToUser(userModel, newEntry);
            if (readdSucceeded) return NoContent();
            else
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"resource creation failed");
                return InternalSeverError();
            }
        }

        /// <summary>
        /// Delete an entry
        /// </summary>
        /// <param name="id">UID of the entry</param>
        /// <param name="owner">Your user data</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromBody] ConnectedUserDTO owner)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");

            var userModel = _data.GetUser(owner.Mail, owner.Password);
            if (userModel == null)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"user {owner.Mail} was not found");
                return Unauthorized();
            }

            var entryModel = _data.GetEntries(userModel).First(e => e.Uid.ToString() == id);
            if (entryModel == null) return NotFound();

            bool removalSucceeded = _data.RemoveEntry(userModel, entryModel);
            if (!removalSucceeded)
            {
                _logger.LogError(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"entry was found in database but could not be removed");
                return InternalSeverError();
            }
            return NoContent();
        }
    }
}