using ChemWebsite.Data;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the customers.
        /// </summary>
        /// <param name="customerResource">The customer resource.</param>
        /// <returns></returns>
        [HttpGet(Name = "GetCustomers")]
        public async Task<IActionResult> GetCustomers([FromQuery] CustomerResource customerResource)
        {
            var query = new GetAllCustomerQuery
            {
                CustomerResource = customerResource
            };
            var customersFromRepo = await _mediator.Send(query);
            var paginationMetadata = new
            {
                totalCount = customersFromRepo.TotalCount,
                pageSize = customersFromRepo.PageSize,
                totalPages = customersFromRepo.TotalPages,
                skip = customersFromRepo.Skip,
            };

            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(customersFromRepo);
        }

        [HttpGet("search",Name = "GetCustomerSearch")]
        public async Task<IActionResult> GetCustomersBySearch([FromQuery] SearchCustomerQuery searchCustomerQuery)
        {
            var customersFromRepo = await _mediator.Send(searchCustomerQuery);
            return Ok(customersFromRepo);
        }

        /// <summary>
        /// Gets the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<IActionResult> GetCustomer(Guid id)
        {
            var query = new GetCustomerQuery { Id = id };
            var response = await _mediator.Send(query);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Creates the customer.
        /// </summary>
        /// <param name="addCustomerCommand">The add customer command.</param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> CreateCustomer([FromBody] AddCustomerCommand addCustomerCommand)
        {
            var response = await _mediator.Send(addCustomerCommand);
            return ReturnFormattedResponse(response);
        }


        /// <summary>
        /// Updates the customer.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateCustomerCommand">The update customer command.</param>
        /// <returns></returns>
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand updateCustomerCommand)
        {
            updateCustomerCommand.Id = id;
            var response = await _mediator.Send(updateCustomerCommand);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Delete Customer By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Check Email for Phone Exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        [HttpGet("{id}/Exist")]
        public async Task<IActionResult> EmailOrPhoneExist(Guid id, string email, string mobileNo)
        {
            var command = new EmailOrPhoneExistCheckQuery
            {
                Email = email,
                MobileNo = mobileNo,
                Id = id
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
