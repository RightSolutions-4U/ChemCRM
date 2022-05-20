using ChemWebsite.Data;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.CustomerChemical
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerChemicalController : BaseController
    {
        private readonly IMediator _mediator;
        public CustomerChemicalController(IMediator mediator)
        {
            _mediator = mediator;
        }



        /// <summary>
        /// Gets the customers by chemical.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("chemical/{id}", Name = "GetCustomersByChemicalId")]
        public async Task<IActionResult> GetCustomersByChemicalId(Guid id)
        {
            var query = new GetCustomersByChemicalQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Adds the customer chemical.
        /// </summary>
        /// <param name="addCustomerChemicalCommand">The add customer chemical command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCustomerChemical([FromBody] AddCustomerChemicalCommand addCustomerChemicalCommand)
        {
            var result = await _mediator.Send(addCustomerChemicalCommand);
            return Ok(result);
        }

        /// <summary>
        /// Saves the supplier chemical.
        /// </summary>
        /// <param name="addSupplierChemicalCommand">The add supplier chemical command.</param>
        /// <returns></returns>
        [HttpPost("chemical/{chemicalId}")]
        public async Task<IActionResult> SaveCustomerByChemical([FromBody] AddCustomerByChemicalIdCommand addCustomerByChemicalIdCommand)
        {
            var result = await _mediator.Send(addCustomerByChemicalIdCommand);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the customer chemical.
        /// </summary>
        /// <param name="chemicalId">The chemical identifier.</param>
        /// <param name="customerId">The customer identifier.</param>
        /// <returns></returns>
        [HttpDelete("{chemicalId}/{customerId}", Name = "DeleteCustomerChemical")]
        public async Task<IActionResult> DeleteCustomerChemical(Guid chemicalId, Guid customerId)
        {
            var command = new DeleteCustomerChemicalCommand
            {
                ChemicalId = chemicalId,
                CustomerId = customerId
            };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        [HttpGet("customer/{id}", Name = "GetChemicalsByCustomerId")]
        public async Task<IActionResult> GetChemicalsByCustomerId(Guid id, int skip = 0, int take = 10, string chemicalName = "", string casNumber = "")
        {
            var query = new GetChemicalsByCustomerQuery
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
        /// Gets the Customers by chemical Id.
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>
        [HttpGet("chemical")]
        public async Task<IActionResult> GetCustomersByChemicalId([FromQuery] CustomerResource customerResource)
        {
            var query = new GetCustomersByChemicalIdQuery
            {
                CustomerResource = customerResource
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
    }
}
