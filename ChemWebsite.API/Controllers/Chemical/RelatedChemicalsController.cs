using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RelatedChemicalsController : ControllerBase
    {
        readonly IMediator _mediator;
        public RelatedChemicalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the related chemicals.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetRelatedChemicals")]
        public async Task<IActionResult> GetRelatedChemicals(Guid id)
        {
            var query = new GetRelatedChemicalsQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
