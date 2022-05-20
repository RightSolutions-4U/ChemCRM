using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.SalesOrder
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesOrderController : BaseController
    {
        private readonly IMediator _mediator;
        public SalesOrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Sales Order.
        /// </summary>
        /// <param name="addSalesOrderCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(SalesOrderDto))]
        public async Task<IActionResult> CreateSalesOrder(AddSalesOrderCommand addSalesOrderCommand)
        {
            var result = await _mediator.Send(addSalesOrderCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get All Sales Order.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSalesOrders([FromQuery] SaleOrderResource saleOrderResource)
        {
            var getAllSalesOrderQuery = new GetAllSalesOrderQuery { SaleOrderResource = saleOrderResource };
            var result = await _mediator.Send(getAllSalesOrderQuery);
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

        /// <summary>
        /// Get New Sales Order.
        /// </summary>
        /// <returns></returns>
        [HttpGet("newOrderNumber")]
        public async Task<IActionResult> GetNewSalesOrderNumber()
        {
            var getNewSalesOrderNumberQuery = new GetNewSalesOrderNumberQuery { };
            var response = await _mediator.Send(getNewSalesOrderNumberQuery);
            return Ok(new
            {
                SalesOrderNumber = response
            });
        }

        /// <summary>
        /// Get Purchase Orders By Sales Order By Id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/purchaseOrder")]
        public async Task<IActionResult> GetPurchaseOrderBySaleOrderId(Guid Id)
        {
            var getPurchaseOrderDetailBySoIdQuery = new GetPurchaseOrderDetailBySoIdQuery
            {
                SalesOrderId = Id
            };
            var response = await _mediator.Send(getPurchaseOrderDetailBySoIdQuery);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Get Sales Order Detail
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetSalesOrderDetail(Guid Id)
        {
            var getSalesOrderDetailQuery = new GetSalesOrderDetailQuery
            {
                Id = Id
            };
            var response = await _mediator.Send(getSalesOrderDetailQuery);
            return ReturnFormattedResponse(response);
        }
        /// <summary>
        /// Get Recenet Expected Shipment Sales Order
        /// </summary>
        /// <returns></returns>

        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentExpectedShipmentDateSalesOrder()
        {
            var getSalesOrderRecentShipmentDateQuery = new GetSalesOrderRecentShipmentDateQuery
            {
            };
            var serviceResponse = await _mediator.Send(getSalesOrderRecentShipmentDateQuery);
            return Ok(serviceResponse);
        }

        /// <summary>
        /// Delete Sales Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesOrder(Guid id)
        {
            var deleteSalesOrderCommand = new DeleteSalesOrderCommand
            {
                Id = id
            };
            var response = await _mediator.Send(deleteSalesOrderCommand);
            return Ok(response);
        }

        /// <summary>
        /// Close Sales Order.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}/close")]
        public async Task<IActionResult> CloseSalesOrder(Guid id)
        {
            var closeSalesOrderCommand = new CloseSalesOrderCommand
            {
                Id = id
            };
            var response = await _mediator.Send(closeSalesOrderCommand);
            return Ok(response);
        }
    }
}
