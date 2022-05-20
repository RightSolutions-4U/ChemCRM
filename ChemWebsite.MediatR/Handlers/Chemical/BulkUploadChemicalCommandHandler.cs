using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class BulkUploadChemicalCommandHandler
        : IRequestHandler<BulkUploadChemicalCommand, ServiceResponse<bool>>
    {
        private readonly IChemicalRepository _chemicalRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<BulkUploadChemicalCommand> _logger;

        public BulkUploadChemicalCommandHandler(
            IChemicalRepository chemicalRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<BulkUploadChemicalCommand> logger)
        {
            _chemicalRepository = chemicalRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(BulkUploadChemicalCommand request, CancellationToken cancellationToken)
        {
            var chemicals = _mapper.Map<List<Chemical>>(request.Chemicals);
            _chemicalRepository.AddRange(chemicals);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Creating a Chemicals  failed on save.", request);
                return ServiceResponse<bool>.ReturnFailed(500, $"Creating a Chemicals failed on save.");
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
