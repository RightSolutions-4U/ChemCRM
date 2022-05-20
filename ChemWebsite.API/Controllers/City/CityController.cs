using ChemWebsite.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChemWebsite.API.Controllers.City
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : BaseController
    {
        readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets the specified country identifier.
        /// </summary>
        /// <param name="countryId">The country identifier.</param>
        /// <param name="cityname">The cityname.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(Guid countryId, string cityname)
        {
            var query = new GetCitiesByContryIdQuery
            {
                CountryId = countryId,
                CityName = cityname
            };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// Gets the cities.
        /// </summary>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="cityName">Name of the city.</param>
        /// <returns></returns>
        [HttpGet("countryName")]
        public async Task<IActionResult> GetCities(string countryName, string cityName)
        {
            var query = new GetCitiesByContryNameQuery
            {
                CityName = cityName,
                CountryName = countryName
            };

            var result = await _mediator.Send(query);
            return ReturnFormattedResponse(result);
        }
    }
}
