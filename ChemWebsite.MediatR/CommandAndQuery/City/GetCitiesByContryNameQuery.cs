using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCitiesByContryNameQuery : IRequest<ServiceResponse<List<CityDto>>>
    {
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }
}
