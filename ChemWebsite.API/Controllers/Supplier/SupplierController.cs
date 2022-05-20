using ChemWebsite.Data.Resources;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers
{
    /// <summary>
    /// Supplier Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : BaseController
    {
        public readonly IMediator _mediator;
        private readonly ILogger<SupplierController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public SupplierController(
            IMediator mediator,
             ILogger<SupplierController> logger

              )
        {
            _mediator = mediator;
            _logger = logger;

        }
        /// <summary>
        /// Get All Suppliers
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>

        [HttpGet(Name = "GetSuppliers")]
        public async Task<IActionResult> GetSuppliers([FromQuery] SupplierResource supplierResource)
        {
            var getAllSupplierQuery = new GetAllSupplierQuery
            {
                SupplierResource = supplierResource
            };
            var result = await _mediator.Send(getAllSupplierQuery);

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
        /// Get Supplier by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSupplier")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var getSupplierQuery = new GetSupplierQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getSupplierQuery);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Add Supplier
        /// </summary>
        /// <param name="addSupplierCommand"></param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierCommand addSupplierCommand)
        {
            var result = await _mediator.Send(addSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return CreatedAtRoute("GetSupplier",
                 new { id = result.Data.Id },
                 result.Data);
        }
        /// <summary>
        /// Update Supplier By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateSupplierCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] UpdateSupplierCommand updateSupplierCommand)
        {
            updateSupplierCommand.Id = id;
            var result = await _mediator.Send(updateSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return NoContent();
        }
        /// <summary>
        /// Delete Supplier By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var deleteSupplierCommand = new DeleteSupplierCommand { Id = id };
            var result = await _mediator.Send(deleteSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Is Email Or Phone Exist for Another supplier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <param name="mobileNo"></param>
        /// <returns></returns>

        [HttpGet("{id}/Exist")]
        public async Task<IActionResult> IsEmailOrPhoneExist(Guid id, string email, string mobileNo)
        {
            var isEmailOrPhoneExistQuery = new IsEmailOrPhoneExistQuery
            {
                EMail = email,
                Id = id,
                Phone = mobileNo
            };
            var result = await _mediator.Send(isEmailOrPhoneExistQuery);
            return Ok(result);
        }
        /// <summary>
        /// Get Latest Register Suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNewSupplier")]
        public async Task<IActionResult> GetNewSupplier()
        {
            var result = await _mediator.Send(new GetNewSupplierQuery { });
            return Ok(result);
        }

    }
}
