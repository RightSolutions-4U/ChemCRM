using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.PackagingType
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class PackagingTypeController : BaseController
    {
        public IMediator _mediator { get; set; }

        public PackagingTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Packaging Type.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PackagingType/{id}", Name = "GetPackagingType")]
        [Produces("application/json", "application/xml", Type = typeof(PackagingTypeDto))]
        public async Task<IActionResult> GetPackagingType(Guid id)
        {
            var getPackagingTypeQuery = new GetPackagingTypeQuery { Id = id };
            var result = await _mediator.Send(getPackagingTypeQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get All Packaging Types.
        /// </summary>
        /// <returns></returns>
        [HttpGet("PackagingTypes")]
        [Produces("application/json", "application/xml", Type = typeof(List<PackagingTypeDto>))]
        public async Task<IActionResult> GetPackagingTypes()
        {
            var getAllPackagingTypeQuery = new GetAllPackagingTypeQuery { };
            var result = await _mediator.Send(getAllPackagingTypeQuery);
            return Ok(result);
        }

        /// <summary>
        /// Add Packaging Type.
        /// </summary>
        /// <param name="addPackagingTypeCommand"></param>
        /// <returns></returns>
        [HttpPost("PackagingType")]
        [Produces("application/json", "application/xml", Type = typeof(PackagingTypeDto))]
        public async Task<IActionResult> AddPackagingType(AddPackagingTypeCommand addPackagingTypeCommand)
        {
            var response = await _mediator.Send(addPackagingTypeCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetPackagingType", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update Packaging Type.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatePackagingTypeCommand"></param>
        /// <returns></returns>
        [HttpPut("PackagingType/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(PackagingTypeDto))]
        public async Task<IActionResult> UpdatePackagingType(Guid Id, UpdatePackagingTypeCommand updatePackagingTypeCommand)
        {
            updatePackagingTypeCommand.Id = Id;
            var result = await _mediator.Send(updatePackagingTypeCommand);
            return ReturnFormattedResponse(result);

        }

        /// <summary>
        /// Delete Packaging type.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("PackagingType/{Id}")]
        public async Task<IActionResult> DeletePackagingType(Guid Id)
        {
            var deletePackagingTypeCommand = new DeletePackagingTypeCommand { Id = Id };
            var result = await _mediator.Send(deletePackagingTypeCommand);
            return ReturnFormattedResponse(result);
        }

    }
}
