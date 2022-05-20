using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers
{
    [Route("api")]
    [ApiController]
    public class PaymentTermController : BaseController
    {
        public IMediator _mediator { get; set; }

        public PaymentTermController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get PaymentTerms.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("PaymentTerm/{id}", Name = "GetPaymentTerm")]
        [Produces("application/json", "application/xml", Type = typeof(PaymentTermDto))]
        public async Task<IActionResult> GetPaymentTerm(Guid id)
        {
            var getPaymentTermQuery = new GetPaymentTermQuery { Id = id };
            var result = await _mediator.Send(getPaymentTermQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get PaymentTerms
        /// </summary>
        /// <returns></returns>
        [HttpGet("PaymentTerms")]
        [Produces("application/json", "application/xml", Type = typeof(List<PaymentTermDto>))]
        public async Task<IActionResult> GetPaymentTerms()
        {
            var getAllPaymentTermQuery = new GetAllPaymentTermQuery { };
            var result = await _mediator.Send(getAllPaymentTermQuery);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Create PaymentTerm.
        /// </summary>
        /// <param name="addPaymentTermCommand"></param>
        /// <returns></returns>
        [HttpPost("PaymentTerm")]
        [Produces("application/json", "application/xml", Type = typeof(PaymentTermDto))]
        public async Task<IActionResult> AddPaymentTerm(AddPaymentTermCommand addPaymentTermCommand)
        {
            var response = await _mediator.Send(addPaymentTermCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetPaymentTerm", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update PaymentTerm.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatePaymentTermCommand"></param>
        /// <returns></returns>
        [HttpPut("PaymentTerm/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(PaymentTermDto))]
        public async Task<IActionResult> UpdateDeliveryMethod(Guid Id, UpdatePaymentTermCommand updatePaymentTermCommand)
        {
            updatePaymentTermCommand.Id = Id;
            var result = await _mediator.Send(updatePaymentTermCommand);
            return ReturnFormattedResponse(result);

        }

        /// <summary>
        /// Delete PaymentTerm.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("PaymentTerm/{Id}")]
        public async Task<IActionResult> DeletePaymentTerm(Guid Id)
        {
            var deletePaymentTermCommand = new DeletePaymentTermCommand { Id = Id };
            var result = await _mediator.Send(deletePaymentTermCommand);
            return ReturnFormattedResponse(result);
        }
    }
}