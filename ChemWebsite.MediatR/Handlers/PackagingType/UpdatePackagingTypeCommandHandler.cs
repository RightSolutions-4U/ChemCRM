using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdatePackagingTypeCommandHandler
        : IRequestHandler<UpdatePackagingTypeCommand, ServiceResponse<PackagingTypeDto>>
    {
        private readonly IPackagingTypeRepository _packagingTypeRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<UpdatePackagingTypeCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdatePackagingTypeCommandHandler(
           IPackagingTypeRepository packagingTypeRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<UpdatePackagingTypeCommandHandler> logger,
            IMapper mapper
            )
        {
            _packagingTypeRepository = packagingTypeRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<PackagingTypeDto>> Handle(UpdatePackagingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packagingTypeRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Packaging Type Already Exist for another Delivery Method.");
                return ServiceResponse<PackagingTypeDto>.Return409("Packaging Type Already Exist for another Delivery Method.");
            }
            existingEntity.Name = request.Name;
            _packagingTypeRepository.Update(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Packaging Type");
                return ServiceResponse<PackagingTypeDto>.Return500();
            }
            return ServiceResponse<PackagingTypeDto>.ReturnResultWith200(_mapper.Map<PackagingTypeDto>(existingEntity));
        }
    }
}
