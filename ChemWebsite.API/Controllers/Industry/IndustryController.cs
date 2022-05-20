using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Command;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.Industry
{
    /// <summary>
    /// IndustryController
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IndustryController : BaseController
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Initializes a new instance of the <see cref="IndustryController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public IndustryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the industries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<IndustryDto>))]
        public async Task<IActionResult> GetIndustries()
        {
            var query = new GetIndustriesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Inserts the industry.
        /// </summary>
        /// <param name="addIndustryCommand">The add industry command.</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(IndustryDto))]
        public async Task<IActionResult> InsertIndustry([FromBody] AddIndustryCommand addIndustryCommand)
        {
            var result = await _mediator.Send(addIndustryCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Updates the industry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateIndustryCommand">The update industry command.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(IndustryDto))]
        public async Task<IActionResult> UpdateIndustry(Guid id, [FromBody] UpdateIndustryCommand updateIndustryCommand)
        {
            updateIndustryCommand.Id = id;
            var result = await _mediator.Send(updateIndustryCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Gets the industry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIndustry(Guid id)
        {
            var query = new GetIndustryQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the industry.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIndustry(Guid id)
        {
            var command = new DeleteIndustryCommand() { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
    }
}
