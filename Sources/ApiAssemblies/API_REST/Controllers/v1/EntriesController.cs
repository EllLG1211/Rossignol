using System.Reflection;
using AutoMapper;
using DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class EntriesController : RossignolControllerBase
    {
        private readonly ILogger<EntriesController> _logger;
        private IMapper _mapper;
        

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

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Add([FromBody] EntryDTO entry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{entry:{entry}}}");
            return StatusCode(501);
        }

        [HttpPut("{id}")]
        public IActionResult Modify(string id, [FromBody] EntryDTO entry)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}, entry: {entry}}}");
            return StatusCode(501);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _logger.LogInformation(LOG_FORMAT, MethodBase.GetCurrentMethod()?.Name, $"input {{id:{id}}}");
            return StatusCode(501);
        }
    }
}