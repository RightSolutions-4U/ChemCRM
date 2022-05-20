using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetCountryQueryHandler : IRequestHandler<GetCountryQuery, List<CountryDto>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public GetCountryQueryHandler(ICountryRepository countryRepository,
            IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<List<CountryDto>> Handle(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var countries = await _countryRepository.All.OrderBy(c => c.CountryName).ToListAsync();
            return _mapper.Map<List<CountryDto>>(countries);
        }
    }
}
