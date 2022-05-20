using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ChemicalController : BaseController
    {

        private readonly IMediator _mediator;
        /// <summary>
        /// Initializes a new instance of the <see cref="ChemicalController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="webHostEnvironment"></param>
        /// <param name="pathHelper"></param>
        public ChemicalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the chemicals.
        /// </summary>
        /// <param name="chemicalResource">The chemical resource parameters.</param>
        /// <returns></returns>
        [HttpGet(Name = "GetChemicals")]
        public async Task<IActionResult> GetChemicals([FromQuery] ChemicalResource chemicalResource)
        {
            var query = new GetChemicalsQuery { ChemicalResource = chemicalResource };
            var chemicalsFromRepo = await _mediator.Send(query);

            var paginationMetadata = new
            {
                totalCount = chemicalsFromRepo.TotalCount,
                pageSize = chemicalsFromRepo.PageSize,
                skip = chemicalsFromRepo.Skip,
                totalPages = chemicalsFromRepo.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(chemicalsFromRepo);
        }

        /// <summary>
        /// Gets the chemical.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetChemical")]
        public async Task<IActionResult> GetChemical(Guid id)
        {
            var query = new GetChemicalQuery { Id = id };
            var response = await _mediator.Send(query);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Creates the chemical.
        /// </summary>
        /// <param name="addChemicalCommand">The add chemical command.</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateChemical([FromBody] AddChemicalCommand addChemicalCommand)
        {
            var result = await _mediator.Send(addChemicalCommand);
            if (!result.Success)
            {
                return ReturnFormattedResponse(result);
            }
            return CreatedAtAction("GetChemical", new { id = result.Data.Id }, result.Data);
        }

        /// <summary>
        /// Updates the chemical.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateChemicalCommand">The update chemical command.</param>
        /// <returns></returns>
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateChemical(Guid id, [FromBody] UpdateChemicalCommand updateChemicalCommand)
        {
            updateChemicalCommand.Id = id;
            var result = await _mediator.Send(updateChemicalCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Deletes the chemical.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChemical(Guid id)
        {
            var command = new DeleteChemicalCommand { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Uploads the chemicals.
        /// </summary>
        /// <param name="chemicals">The chemicals.</param>
        /// <returns></returns>
        [HttpPost("bulkupload")]
        public async Task<IActionResult> UploadChemicals(List<ChemicalDto> chemicals)
        {
            var command = new BulkUploadChemicalCommand
            {
                Chemicals = chemicals
            };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
    }
}
