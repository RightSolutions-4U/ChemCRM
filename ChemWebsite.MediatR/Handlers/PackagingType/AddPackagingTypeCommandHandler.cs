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
    public class AddPackagingTypeCommandHandler 
        : IRequestHandler<AddPackagingTypeCommand, ServiceResponse<PackagingTypeDto>>
    {
        private readonly IPackagingTypeRepository _packagingTypeRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPackagingTypeCommandHandler> _logger;
        public AddPackagingTypeCommandHandler(
           IPackagingTypeRepository packagingTypeRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddPackagingTypeCommandHandler> logger
            )
        {
            _packagingTypeRepository = packagingTypeRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PackagingTypeDto>> Handle(AddPackagingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packagingTypeRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Packaging Type Already Exist");
                return ServiceResponse<PackagingTypeDto>.Return409("Packaging Type Already Exist.");
            }
            var entity = _mapper.Map<PackagingType>(request);
            entity.Id = Guid.NewGuid();
            _packagingTypeRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Packaging Type.");
                return ServiceResponse<PackagingTypeDto>.Return500();
            }
            return ServiceResponse<PackagingTypeDto>.ReturnResultWith200(_mapper.Map<PackagingTypeDto>(entity));
        }
    }
}
