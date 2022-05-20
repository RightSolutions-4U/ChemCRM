using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handler
{
    public class GetIndustryQueryHandler : IRequestHandler<GetIndustryQuery, IndustryDto>
    {
        private readonly IIndustryRepository _industryRepository;
        private readonly IMapper _mapper;
        public GetIndustryQueryHandler(
            IIndustryRepository industryRepository,
            IMapper mapper)
        {
            _industryRepository = industryRepository;
            _mapper = mapper;
        }

        public async Task<IndustryDto> Handle(GetIndustryQuery request, CancellationToken cancellationToken)
        {
            var industryEntity = _industryRepository.Find(request.Id);
            var industryDto = _mapper.Map<IndustryDto>(industryEntity);
            return await Task.FromResult(industryDto);
        }
    }
}
