using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetChemicalTypeQueryHandler : IRequestHandler<GetChemicalTypeQuery, ServiceResponse<ChemicalTypeDto>>
    {
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetChemicalTypeQueryHandler> _logger;

        public GetChemicalTypeQueryHandler(
            IChemicalTypeRepository chemicalTypeRepository,
            IMapper mapper,
            ILogger<GetChemicalTypeQueryHandler> logger)
        {
            _chemicalTypeRepository = chemicalTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<ChemicalTypeDto>> Handle(GetChemicalTypeQuery request, CancellationToken cancellationToken)
        {
            var entity = await _chemicalTypeRepository.FindAsync(request.Id);
            if (entity == null)
            {
                _logger.LogError("Chemical Type Not found", request);
                return ServiceResponse<ChemicalTypeDto>.Return404("Chemical Type Not found");
            }
            return ServiceResponse<ChemicalTypeDto>.ReturnResultWith200(_mapper.Map<ChemicalTypeDto>(entity));
        }
    }
}
