using System.ComponentModel.DataAnnotations;
using Application;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace TreeNodeApi.Controllers
{
    [Route("api/journal")]
    [ApiController]
    public class JournalController : Controller
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(IJournalService journalService, IMapper mapper)
        {
            _journalService = journalService;
            _mapper = mapper;
        }
        [HttpGet("${id}")]
        public async Task<ActionResult<Journal>> Get(int id)
        {
            return await _journalService.Get(id);
        }


        [HttpPost("getRange")]
        public async Task<PaginatedJournals> GetRange([Required] int skip, [Required] int take, [FromBody, Required] Filter filter)
        {
            return await _journalService.GetRange(skip, take, filter.From, filter.To);
        }
    }
}
