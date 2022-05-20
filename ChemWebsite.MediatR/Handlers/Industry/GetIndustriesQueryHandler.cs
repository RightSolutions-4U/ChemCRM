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

namespace ChemWebsite.MediatR.Handler
{
    public class GetIndustriesQueryHandler : IRequestHandler<GetIndustriesQuery, List<IndustryDto>>
    {
        private readonly IIndustryRepository _industryRepository;
        private readonly IMapper _mapper;
        public GetIndustriesQueryHandler(
            IIndustryRepository industryRepository,
            IMapper mapper)
        {
            _industryRepository = industryRepository;
            _mapper = mapper;
        }
        public async Task<List<IndustryDto>> Handle(GetIndustriesQuery request, CancellationToken cancellationToken)
        {
            var industriesEnitity = await _industryRepository.All.Where(cc => !cc.IsDeleted).ToListAsync();
            var indutryiesDto = _mapper.Map<List<IndustryDto>>(industriesEnitity);
            return indutryiesDto;
        }
    }
}
