using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddChemicalTypeQueryHandler : IRequestHandler<AddChemicalTypeCommand, ServiceResponse<ChemicalTypeDto>>
    {
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddChemicalTypeQueryHandler> _logger;

        public AddChemicalTypeQueryHandler(
            IChemicalTypeRepository chemicalTypeRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            ILogger<AddChemicalTypeQueryHandler> logger)
        {
            _chemicalTypeRepository = chemicalTypeRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<ChemicalTypeDto>> Handle(AddChemicalTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<ChemicalType>(request);
            _chemicalTypeRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while adding Chemical Type.");
                return ServiceResponse<ChemicalTypeDto>.Return500();
            }
            var entityDto = _mapper.Map<ChemicalTypeDto>(entity);
            return ServiceResponse<ChemicalTypeDto>.ReturnResultWith200(entityDto);
        }
    }
}
