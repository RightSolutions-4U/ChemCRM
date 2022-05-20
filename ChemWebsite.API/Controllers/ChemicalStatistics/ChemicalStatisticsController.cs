using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.ChemicalStatistics
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChemicalStatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChemicalStatisticsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the most viewed chemicals.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMostViewedChemicals")]
        public async Task<IActionResult> GetMostViewedChemicals()
        {
            var query = new GetMostViewedChemicalQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets the most searched chemical.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMostSearchedChemicals")]
        public async Task<IActionResult> GetMostSearchedChemical()
        {
            var query = new GetMostSearchedChemicalQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Increases the top search count.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("IncreaseTopSearchCount")]
        public async Task<IActionResult> IncreaseTopSearchCount(Guid id)
        {
            var command = new IncreseMostSearchedChemicalCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Increase the most viewed chemical.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [HttpGet("IncreseMostViewedChemical")]
        public async Task<IActionResult> IncreaseMostViewedChemical(Guid id)
        {
            var command = new IncreaseMostViewedChemicalCommand { Id = id };
            await _mediator.Send(command);
            return Ok();
        }
    }
}
