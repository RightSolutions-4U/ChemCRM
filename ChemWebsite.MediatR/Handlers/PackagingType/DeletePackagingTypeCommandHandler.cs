using ChemWebsite.Common.UnitOfWork;
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
    public class DeletePackagingTypeCommandHandler
        : IRequestHandler<DeletePackagingTypeCommand, ServiceResponse<bool>>
    {
        private readonly IPackagingTypeRepository _packagingTypeRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeletePackagingTypeCommandHandler> _logger;

        public DeletePackagingTypeCommandHandler(
           IPackagingTypeRepository packagingTypeRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeletePackagingTypeCommandHandler> logger
            )
        {
            _packagingTypeRepository = packagingTypeRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePackagingTypeCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _packagingTypeRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (existingEntity == null)
            {
                _logger.LogError("Packaging Type does not Exist");
                return ServiceResponse<bool>.Return409("Packaging Type does not  Exist.");
            }
            _packagingTypeRepository.Remove(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Packaging Type.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
