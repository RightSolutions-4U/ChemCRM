using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.Country
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets countries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetCountryQuery();
            var country = await _mediator.Send(query);
            return Ok(country);
        }
    }
}
