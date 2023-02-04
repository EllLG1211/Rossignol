using System.Reflection;
using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class SharedEntriesController : RossignolControllerBase
    {
        private readonly ILogger<SharedEntriesController> _logger;
        private IMapper _mapper;

        public SharedEntriesController(ILogger<SharedEntriesController> logger, IMapper mapper)
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

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SharedEntryDTO sharedEntry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{sharedEntry:{sharedEntry}}}");
            return StatusCode(501);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SharedEntryDTO sharedEntry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{sharedEntry:{sharedEntry}}}");
            return StatusCode(501);
        }
    }
}