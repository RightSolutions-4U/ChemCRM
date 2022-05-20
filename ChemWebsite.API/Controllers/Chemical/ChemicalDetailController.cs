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
    /// <seealso cref="ChemWebsite.API.Controllers.BaseController" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChemicalDetailController : BaseController
    {
        public readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChemicalDetailController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public ChemicalDetailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Chemical Detail By Name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetChemicalByName(string name)
        {
            var query = new GetChemicalByNameQuery { Name = name };
            var result = await _mediator.Send(query);
            return ReturnFormattedResponse(result);
        }
    }
}
