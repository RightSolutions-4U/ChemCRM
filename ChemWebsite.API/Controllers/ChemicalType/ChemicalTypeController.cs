using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.ChemicalType
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChemicalTypeController : BaseController
    {
        public IMediator _mediator { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        /// <param name="mediator"></param>
        public ChemicalTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get Chemical Type By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("{id}", Name = "GetChemicalType")]
        [Produces("application/json", "application/xml", Type = typeof(ChemicalTypeDto))]
        public async Task<IActionResult> GetChemicalType(Guid id)
        {
            var getChemicalTypeQuery = new GetChemicalTypeQuery { Id = id };
            var result = await _mediator.Send(getChemicalTypeQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Get All Chemical Types
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [Produces("application/json", "application/xml", Type = typeof(List<ChemicalTypeDto>))]
        public async Task<IActionResult> GetChemicalTypes()
        {
            var getAllChemicalTypeQuery = new GetAllChemicalTypeQuery { };
            var result = await _mediator.Send(getAllChemicalTypeQuery);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Create A Chemical Type
        /// </summary>
        /// <param name="AddChemicalTypeCommand"></param>
        /// <returns></returns>
        [HttpPost()]
        [Produces("application/json", "application/xml", Type = typeof(ChemicalTypeDto))]
        public async Task<IActionResult> AddChemicalType(AddChemicalTypeCommand addChemicalTypeCommand)
        {
            var response = await _mediator.Send(addChemicalTypeCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetChemicalType", new { id = response.Data.Id }, response.Data);
        }
        /// <summary>
        /// Update Exist Chemical Type By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="UpdateChemicalTypeCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(ChemicalTypeDto))]
        public async Task<IActionResult> UpdateChemicalType(Guid Id, UpdateChemicalTypeCommand updateChemicalTypeCommand)
        {
            updateChemicalTypeCommand.Id = Id;
            var result = await _mediator.Send(updateChemicalTypeCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Delete Chemical Type By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteChemicalType(Guid Id)
        {
            var deleteChemicalTypeCommand = new DeleteChemicalTypeCommand { Id = Id };
            var result = await _mediator.Send(deleteChemicalTypeCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
