using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.PurchaseOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseOrderController : BaseController
    {
        public IMediator _mediator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseOrderController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public PurchaseOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets all purchase order.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(List<PurchaseOrderDto>))]
        public async Task<IActionResult> GetAllPurchaseOrder([FromQuery] PurchaseOrderResource purchaseOrderResource)
        {
            var getAllPurchaseOrderQuery = new GetAllPurchaseOrderQuery
            {
                PurchaseOrderResource = purchaseOrderResource
            };
            var purchaseOrders = await _mediator.Send(getAllPurchaseOrderQuery);

            var paginationMetadata = new
            {
                totalCount = purchaseOrders.TotalCount,
                pageSize = purchaseOrders.PageSize,
                skip = purchaseOrders.Skip,
                totalPages = purchaseOrders.TotalPages
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(purchaseOrders);
        }

        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(List<PurchaseOrderDto>))]
        public async Task<IActionResult> GetPurchaseOrder(Guid id)
        {
            var getPurchaseOrderQuery = new GetPurchaseOrderQuery
            {
                Id = id
            };
            var purchaseOrder = await _mediator.Send(getPurchaseOrderQuery);
            return ReturnFormattedResponse(purchaseOrder);
        }


        /// <summary>
        /// Creates the purchase order.
        /// </summary>
        /// <param name="addPurchaseOrderCommand">The add purchase order command.</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(PurchaseOrderDto))]
        public async Task<IActionResult> CreatePurchaseOrder(AddPurchaseOrderCommand addPurchaseOrderCommand)
        {
            var result = await _mediator.Send(addPurchaseOrderCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Gets the Purchase Order delivery schedules.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}/deliveryschedules")]
        public async Task<IActionResult> GetPODeliverySchedules(Guid id)
        {
            var getAllPurchaseOrderQuery = new GetPurchaseOrderDeliveryScheduleQuery
            {
                PurchaseOrderId = id
            };
            var purchaseOrderDelieverySchedules = await _mediator.Send(getAllPurchaseOrderQuery);
            return Ok(purchaseOrderDelieverySchedules);
        }

        /// <summary>
        /// Delete Purchase Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrder(Guid id)
        {
            var deletePurchaseOrderCommand = new DeletePurchaseOrderCommand
            {
                Id = id
            };
            var response = await _mediator.Send(deletePurchaseOrderCommand);
            return Ok(response);
        }

        /// <summary>
        /// Closes the purchase order.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}/close")]
        public async Task<IActionResult> ClosePurchaseOrder(Guid id)
        {
            var closePurchaseOrderCommand = new ClosePurchaseOrderCommand
            {
                Id = id
            };
            var response = await _mediator.Send(closePurchaseOrderCommand);
            return Ok(response);
        }

        /// <summary>
        /// Gets the new purchase order number.
        /// </summary>
        /// <returns></returns>
        [HttpGet("newOrderNumber")]
        public async Task<IActionResult> GetNewPurchaseOrderNumber()
        {
            var getNewPurchaseOrderNumberQuery = new GetNewPurchaseOrderNumberQuery { };
            var response = await _mediator.Send(getNewPurchaseOrderNumberQuery);
            return Ok(new
            {
                OrderNumber = response
            });
        }

        /// <summary>
        /// Get Avilable Purchase Order for Chemical.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("chemical/{id}")]
        public async Task<IActionResult> GetAvailablePurchaseOrdersForChemical(Guid id)
        {
            var getAvailablePOForChemical = new GetAvailablePOForChemicalQuery { ChemicalId = id };
            var response = await _mediator.Send(getAvailablePOForChemical);
            return Ok(response);
        }

        /// <summary>
        /// Get Inventory List
        /// </summary>
        /// <param name="inventoryResource"></param>
        /// <returns></returns>
        [HttpGet("inventory/all")]
        public async Task<IActionResult> GetInventoryList([FromQuery] GetAllInventoryQuery inventoryResource)
        {
            var result = await _mediator.Send(inventoryResource);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

    }
}
