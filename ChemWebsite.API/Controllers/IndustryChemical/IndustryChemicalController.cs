using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.IndustryChemical
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IndustryChemicalController : BaseController
    {
        private readonly IMediator _mediator;

        public IndustryChemicalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the chemicals by industry identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="chemicalName">Name of the chemical.</param>
        /// <param name="casNumber">The cas number.</param>
        /// <returns></returns>
        [HttpGet("industry/{id}", Name = "GetChemicalsByIndustryId")]
        public async Task<IActionResult> GetChemicalsByIndustryId(Guid id, int skip = 0, int take = 10, string chemicalName = "", string casNumber = "")
        {
            var query = new GetChemicalsByIndustryQuery
            {
                Id = id,
                Skip = skip,
                Take = take,
                ChemicalName = chemicalName,
                CasNumber = casNumber
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Saves the industry chemical.
        /// </summary>
        /// <param name="addIndustryChemicalCommand">The add industry chemical command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveIndustryChemical([FromBody] AddIndustryChemicalCommand addIndustryChemicalCommand)
        {
            var response = await _mediator.Send(addIndustryChemicalCommand);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Deletes the industry chemical.
        /// </summary>
        /// <param name="chemicalId">The chemical identifier.</param>
        /// <param name="industryId">The industry identifier.</param>
        /// <returns></returns>
        [HttpDelete("{chemicalId}/{industryId}", Name = "DeleteIndustryChemical")]
        public async Task<IActionResult> DeleteIndustryChemical(Guid chemicalId, Guid industryId)
        {
            var command = new DeleteIndustryChemicalCommand
            {
                ChemicalId = chemicalId,
                IndustryId = industryId
            };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
    }
}
