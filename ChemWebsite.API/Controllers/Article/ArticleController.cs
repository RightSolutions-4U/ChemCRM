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

namespace ChemWebsite.API.Controllers.Article
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ArticleController : BaseController
    {
        IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Adds the article.
        /// </summary>
        /// <param name="addArticleCommand">The add article command.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddArticle([FromBody] AddArticleCommand addArticleCommand)
        {
            var result = await _mediator.Send(addArticleCommand);
            if (!result.Success)
            {
                return StatusCode(result.StatusCode, result);
            }
            return CreatedAtRoute("GetArticle", new { id = result.Data.Id }, result.Data);
        }

        /// <summary>
        /// Updates the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="updateArticleCommand">The update article command.</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(Guid id, [FromBody] UpdateArticleCommand updateArticleCommand)
        {
            updateArticleCommand.Id = id;
            var result = await _mediator.Send(updateArticleCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Gets the articles.
        /// </summary>
        /// <param name="articleResource">The article resource.</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticles([FromQuery] ArticleResource articleResource)
        {

            var getAllInquiryQuery = new GetAllArticleQuery
            {
                ArticleResource = articleResource
            };
            var result = await _mediator.Send(getAllInquiryQuery);

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
        /// Gets the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetArticle")]
        public async Task<IActionResult> GetArticle(Guid id)
        {
            var query = new GetArticleQuery { Id = id };
            var result = await _mediator.Send(query);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Gets the article by URL.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        [HttpGet("url/{name}", Name = "GetArticleByUrl")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticleByUrl(string name)
        {
            var query = new GetArticleByUrlQuery { Url = name };
            var result = await _mediator.Send(query);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Deletes the article.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(Guid id)
        {
            var command = new DeleteArticleCommand() { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Gets the article categories.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetArticleCategories")]
        public async Task<IActionResult> GetArticleCategories()
        {
            var getAllInquiryQuery = new GetAllArticleCategoriesQuery { };
            var result = await _mediator.Send(getAllInquiryQuery);
            return Ok(result);
        }
    }
}
