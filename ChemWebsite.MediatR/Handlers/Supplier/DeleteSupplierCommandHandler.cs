using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
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
    public class DeleteSupplierCommandHandler : IRequestHandler<DeleteSupplierCommand, ServiceResponse<bool>>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddSupplierCommandHandler> _logger;
        private readonly DashboardStatics _dashboardStatics;
        public DeleteSupplierCommandHandler(ISupplierRepository supplierRepository,
            ILogger<AddSupplierCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            DashboardStatics dashboardStatics
            )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
            _logger = logger;
            _dashboardStatics = dashboardStatics;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var entity = await _supplierRepository.FindAsync(request.Id);
            if (entity == null)
            {
                _logger.LogError("Supplier is not exist");
                return ServiceResponse<bool>.Return422("Supplier does not exist");
            }
            _supplierRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error to Delete Supplier");
                return ServiceResponse<bool>.Return500();
            }
            _dashboardStatics.SupplierCount = _dashboardStatics.SupplierCount - 1;
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
