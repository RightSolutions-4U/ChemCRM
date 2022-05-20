using ChemWebsite.Data.Resources;
using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.SupplierChemical
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierChemicalController : BaseController
    {
        private readonly IMediator _mediator;

        public SupplierChemicalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the chemicals by supplier Id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="take">The take.</param>
        /// <param name="chemicalName"></param>
        /// <param name="casNumber"></param>
        /// <returns></returns>
        [HttpGet("supplier/{id}", Name = "GetChemicalsBySupplierId")]
        public async Task<IActionResult> GetChemicalsBySupplierId(Guid id, int skip = 0, int take = 10, string chemicalName = "", string casNumber = "")
        {
            var query = new GetChemicalsBySupplierQuery
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
        /// Gets the supplier by chemical Id.
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>
        [HttpGet("chemical", Name = "GetSupplierByChemicalId")]
        public async Task<IActionResult> GetSupplierByChemicalId([FromQuery] SupplierResource supplierResource)
        {
            var query = new GetSupplierByChemicalQuery
            {
                SupplierResource = supplierResource
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        /// <summary>
        /// Saves the supplier chemical.
        /// </summary>
        /// <param name="addSupplierChemicalCommand">The add supplier chemical command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveSupplierChemical([FromBody] AddSupplierChemicalCommand addSupplierChemicalCommand)
        {
            var response = await _mediator.Send(addSupplierChemicalCommand);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Saves the supplier chemical.
        /// </summary>
        /// <param name="addSupplierChemicalCommand">The add supplier chemical command.</param>
        /// <returns></returns>
        [HttpPost("chemical/{chemicalId}")]
        public async Task<IActionResult> SaveSupplierByChemical([FromBody] AddSupplierByChemicalIdCommand addSupplierByChemicalIdCommand)
        {
            var result = await _mediator.Send(addSupplierByChemicalIdCommand);
            return Ok(result);
        }

        /// <summary>
        /// Deletes the supplier chemical.
        /// </summary>
        /// <param name="chemicalId">The chemical identifier.</param>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns></returns>
        [HttpDelete("{chemicalId}/{supplierId}", Name = "DeleteSupplierChemical")]
        public async Task<IActionResult> DeleteSupplierChemical(Guid chemicalId, Guid supplierId)
        {
            var command = new DeleteSupplierChemicalCommand
            {
                ChemicalId = chemicalId,
                SupplierId = supplierId
            };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }
    }
}
