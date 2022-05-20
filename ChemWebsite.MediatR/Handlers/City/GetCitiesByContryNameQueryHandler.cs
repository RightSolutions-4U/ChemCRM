using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetCitiesByContryNameQueryHandler : IRequestHandler<GetCitiesByContryNameQuery, ServiceResponse<List<CityDto>>>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;

        public GetCitiesByContryNameQueryHandler(ICityRepository cityRepository,
            ICountryRepository countryRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<CityDto>>> Handle(GetCitiesByContryNameQuery request, CancellationToken cancellationToken)
        {
            Guid countryId = Guid.NewGuid();
            if (string.IsNullOrEmpty(request.CountryName))
            {
                return ServiceResponse<List<CityDto>>.Return404();
            }

            countryId = await _countryRepository
                  .FindBy(c => c.CountryName == request.CountryName)
                  .Select(c => c.Id)
                  .FirstOrDefaultAsync();

            var cities = new List<City>();
            if (string.IsNullOrEmpty(request.CityName))
            {
                cities = await _cityRepository.All
                        .OrderBy(c => c.CityName)
                        .Where(c => c.CountryId == countryId)
                        .Take(100)
                        .ToListAsync();
            }
            else
            {
                cities = await _cityRepository.All
                        .OrderBy(c => c.CityName)
                        .Where(c => c.CountryId == countryId && EF.Functions.Like(c.CityName, $"{request.CityName}%"))
                        .Take(100)
                        .ToListAsync();
            }
            return ServiceResponse<List<CityDto>>.ReturnResultWith200(_mapper.Map<List<CityDto>>(cities));
        }
    }
}
