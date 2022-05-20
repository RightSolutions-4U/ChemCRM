using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.PurchaseOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseOrderDeliveryScheduleController : BaseController
    {
        public IMediator _mediator { get; set; }

        public PurchaseOrderDeliveryScheduleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates the delivery schedule.
        /// </summary>
        /// <param name="createDeliveryScheduleCommand">The create delivery schedule command.</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(PurchaseOrderDeliveryScheduleDto))]
        public async Task<IActionResult> CreateDeliverySchedule(CreateDeliveryScheduleCommand createDeliveryScheduleCommand)
        {
            var result = await _mediator.Send(createDeliveryScheduleCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Updates the delivery schedule.
        /// </summary>
        /// <param name="updateDeliveryScheduleCommand">The update delivery schedule command.</param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json", "application/xml", Type = typeof(PurchaseOrderDeliveryScheduleDto))]
        public async Task<IActionResult> UpdateDeliverySchedule(UpdateDeliveryScheduleCommand updateDeliveryScheduleCommand)
        {
            var result = await _mediator.Send(updateDeliveryScheduleCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Deletes the delivery schedule.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeliverySchedule(Guid id)
        {
            var deleteDeliveryScheduleCommand = new DeleteDeliveryScheduleCommand
            {
                Id = id
            };
            var serviceResponse = await _mediator.Send(deleteDeliveryScheduleCommand);
            return ReturnFormattedResponse(serviceResponse);
        }
        /// <summary>
        /// Get Recent Expected Date Purchase Order
        /// </summary>
        /// <returns></returns>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentExpectedDatePurchaseOrder()
        {
            var getPurchaseOrderRecentDeliveryScheduleQuery = new GetPurchaseOrderRecentDeliveryScheduleQuery
            {
            };
            var serviceResponse = await _mediator.Send(getPurchaseOrderRecentDeliveryScheduleQuery);
            return Ok(serviceResponse);
        }
    }
}
