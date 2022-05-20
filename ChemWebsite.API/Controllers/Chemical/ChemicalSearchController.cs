using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.Chemical
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ChemicalSearch : ControllerBase
    {
        private readonly IMediator  _mediator;
        /// <summary>
        /// Initializes a new instance of the <see cref="ChemicalSearch"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ChemicalSearch(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Searches the chemicals.
        /// </summary>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="searchBy">The search by.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        [HttpGet(Name = "SearchChemicals")]
        public async Task<IActionResult> SearchChemicals(string searchQuery, string searchBy, int pageSize)
        {
            var query = new SearchChecmicalQuery
            {
                SearchBy = searchBy,
                PageSize = pageSize,
                SearchQuery = searchQuery
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
