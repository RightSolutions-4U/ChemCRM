using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllChemicalTypeQueryHandler : IRequestHandler<GetAllChemicalTypeQuery, ServiceResponse<List<ChemicalTypeDto>>>
    {
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllChemicalTypeQueryHandler> _logger;

        public GetAllChemicalTypeQueryHandler(
            IChemicalTypeRepository chemicalTypeRepository,
            IMapper mapper,
            ILogger<GetAllChemicalTypeQueryHandler> logger)
        {
            _chemicalTypeRepository = chemicalTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<List<ChemicalTypeDto>>> Handle(GetAllChemicalTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _chemicalTypeRepository.All.ToListAsync();
            return ServiceResponse<List<ChemicalTypeDto>>.ReturnResultWith200(_mapper.Map<List<ChemicalTypeDto>>(entities));
        }
    }
}
