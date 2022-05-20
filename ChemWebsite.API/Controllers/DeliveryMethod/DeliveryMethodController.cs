using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.DeliveryMethod
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DeliveryMethodController : BaseController
    {
        public IMediator _mediator { get; set; }

        public DeliveryMethodController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Delivery method.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DeliveryMethod/{id}", Name = "GetDeliveryMethod")]
        [Produces("application/json", "application/xml", Type = typeof(DeliveryMethodDto))]
        public async Task<IActionResult> GetDeliveryMethod(Guid id)
        {
            var getDeliveryMethodQuery = new GetDeliveryMethodQuery { Id = id };
            var result = await _mediator.Send(getDeliveryMethodQuery);
            return ReturnFormattedResponse(result);
        }
        
        /// <summary>
        /// Get Delivery methods
        /// </summary>
        /// <returns></returns>
        [HttpGet("DeliveryMethods")]
        [Produces("application/json", "application/xml", Type = typeof(List<DeliveryMethodDto>))]
        public async Task<IActionResult> GetDeliveryMethods()
        {
            var getAllDeliveryMethodQuery = new GetAllDeliveryMethodQuery { };
            var result = await _mediator.Send(getAllDeliveryMethodQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create Delivery Method.
        /// </summary>
        /// <param name="addDeliveryMethodCommand"></param>
        /// <returns></returns>
        [HttpPost("DeliveryMethod")]
        [Produces("application/json", "application/xml", Type = typeof(DeliveryMethodDto))]
        public async Task<IActionResult> AddDeliveryMethod(AddDeliveryMethodCommand addDeliveryMethodCommand)
        {
            var response = await _mediator.Send(addDeliveryMethodCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetDeliveryMethod", new { id = response.Data.Id }, response.Data);
        }
        
        /// <summary>
        /// Update Delivery method.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateDeliveryMethodCommand"></param>
        /// <returns></returns>
        [HttpPut("DeliveryMethod/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(DeliveryMethodDto))]
        public async Task<IActionResult> UpdateDeliveryMethod(Guid Id, UpdateDeliveryMethodCommand updateDeliveryMethodCommand)
        {
            updateDeliveryMethodCommand.Id = Id;
            var result = await _mediator.Send(updateDeliveryMethodCommand);
            return ReturnFormattedResponse(result);

        }
       
        /// <summary>
        /// Delete Delivery method.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DeliveryMethod/{Id}")]
        public async Task<IActionResult> DeleteDeliveryMethod(Guid Id)
        {
            var deleteDeliveryMethodCommand = new DeleteDeliveryMethodCommand { Id = Id };
            var result = await _mediator.Send(deleteDeliveryMethodCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
