using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteChemicalTypeCommandHandler : IRequestHandler<DeleteChemicalTypeCommand, ServiceResponse<bool>>
    {
        private readonly IChemicalTypeRepository _chemicalTypeRepository;
        private readonly ILogger<DeleteChemicalTypeCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public DeleteChemicalTypeCommandHandler(
            IChemicalTypeRepository chemicalTypeRepository,
            ILogger<DeleteChemicalTypeCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _chemicalTypeRepository = chemicalTypeRepository;
            _logger = logger;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteChemicalTypeCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _chemicalTypeRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Not found");
                return ServiceResponse<bool>.Return404();
            }
            _chemicalTypeRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While updating Chemical type");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
