using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChemWebsite.API.Controllers
{
    /// <summary>
    /// Category
    /// </summary>
    [Route("api")]
    [ApiController]
    [Authorize]
    public class DocumentCategoryController : BaseController
    {
        public IMediator _mediator { get; set; }
        /// <summary>
        /// Category
        /// </summary>
        /// <param name="mediator"></param>
        public DocumentCategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get Specific Category by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("DocumentCategory/{id}", Name = "GetCategory")]
        [Produces("application/json", "application/xml", Type = typeof(DocumentCategoryDto))]
        public async Task<IActionResult> GetDocumentCategory(Guid id)
        {
            var getCategoryQuery = new GetDocumentCategoryQuery
            {
                Id = id
            };
            var result = await _mediator.Send(getCategoryQuery);
            return Ok(result);

          
        }
        /// <summary>
        /// Get All Categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet("DocumentCategories")]
        [Produces("application/json", "application/xml", Type = typeof(List<CategoryDto>))]
        public async Task<IActionResult> GetDocumentCategories()
        {
            var getAllCategoryQuery = new GetAllDocumentCategoryQuery { };
            var result = await _mediator.Send(getAllCategoryQuery);
            return Ok(result);
        }
        /// <summary>
        /// Create a DocumentCategory.
        /// </summary>
        [HttpPost("DocumentCategory")]
        [Produces("application/json", "application/xml", Type = typeof(DocumentCategoryDto))]
        public async Task<IActionResult> AddDocumentCategory([FromBody] AddDocumentCategoryCommand addCategoryCommand)
        {
            var result = await _mediator.Send(addCategoryCommand);
            return ReturnFormattedResponse(result);
        }
        /// <summary>
        /// Update Category.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateDocumentCategoryCommand"></param>
        /// <returns></returns>
        [HttpPut("DocumentCategory/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(DocumentCategoryDto))]
        public async Task<IActionResult> UpdateCategory(Guid Id, [FromBody] UpdateDocumentCategoryCommand category)
        {
            category.Id = Id;
            var result = await _mediator.Send(category);
            return ReturnFormattedResponse(result);

        }
        /// <summary>
        /// Delete Category.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("DocumentCategory/{Id}")]
        public async Task<IActionResult> DeleteDocumentCategory(Guid Id)
        {
            var deleteCategoryCommand = new DeleteDocumentCategoryCommand
            {
                Id = Id
            };
            var result = await _mediator.Send(deleteCategoryCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
