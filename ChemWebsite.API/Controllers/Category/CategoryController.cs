using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers
{
    /// <summary>
    /// Category Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public CategoryController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Categories
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(int))]
        public async Task<IActionResult> GetCategories(int pageSize = 50, int pageNumber = 1)
        {
            var query = new GetCategoriesQuery
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Categories With Chemicals
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCategoriesWithChemicals")]
        public async Task<IActionResult> GetCategoriesWithChemicals()
        {
            var query = new GetCategoriesWithChemicalsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Get Category Chemical with Pagination
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet("GetCategoryChemicalWithPagination")]
        public async Task<IActionResult> GetCategoryChemicalWithPagination(Guid categoryId, int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetCategoryChemicalWithPaginationQuery
            {
                CategoryId = categoryId,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

    }
}
