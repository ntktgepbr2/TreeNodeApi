using System.ComponentModel.DataAnnotations;
using Application;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TreeNodeApi.Contracts;

namespace TreeNodeApi.Controllers
{
    /// <summary>
    /// Journal controller.
    /// </summary>
    [Route("api/journal")]
    [ApiController]
    public class JournalController : Controller
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Journal controller constructor.
        /// </summary>
        /// <param name="journalService">The journal service.</param>
        /// <param name="mapper">The mapper</param>
        public JournalController(IJournalService journalService, IMapper mapper)
        {
            _journalService = journalService;
            _mapper = mapper;
        }
        /// <summary>
        /// Get journal.
        /// </summary>
        /// <param name="id">The journal id</param>
        /// <returns>
        /// The journal.
        /// </returns>
        [SwaggerOperation(Description = "Returns the information about an particular event by ID.")]
        [HttpGet("getSingle")]
        public async Task<ActionResult<Journal>> Get([Required] int id)
        {
            return await _journalService.Get(id);
        }

        /// <summary>
        /// Get journals by date.
        /// </summary>
        /// <param name="skip">Amount to skip.</param>
        /// <param name="take">Amount to take.</param>
        /// <param name="filter">Filter journals by date.</param>
        /// <returns></returns>
        [SwaggerOperation(Description = "Provides the pagination API. Skip means the number of items should be skipped by server. Take means the maximum number items should be returned by server. All fields of the filter are optional.")]
        [HttpPost("getRange")]
        public async Task<PaginatedJournals> GetRange([Required] int skip, [Required] int take, [FromBody, Required] Filter filter)
        {
            return await _journalService.GetRange(skip, take, filter.From, filter.To);
        }
    }
}
