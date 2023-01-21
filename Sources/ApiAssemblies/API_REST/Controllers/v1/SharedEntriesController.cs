using API_REST.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace API_REST.Controllers.V1
{
    [ApiController]
    public class SharedEntriesController : RossignolControllerBase
    {
        [Inject]
        public IMapper Mapper { get; init; }

        [HttpGet]
        public IActionResult List(int page = 1)
        {
            return StatusCode(501);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return StatusCode(501);
        }

        [HttpPost]
        public IActionResult Add([FromBody] SharedEntryDTO sharedEntry)
        {
            return StatusCode(501);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody] SharedEntryDTO sharedEntry)
        {
            return StatusCode(501);
        }
    }
}