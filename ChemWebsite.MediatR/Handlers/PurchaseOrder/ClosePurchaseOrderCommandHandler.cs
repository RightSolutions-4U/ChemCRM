using ChemWebsite.Common.UnitOfWork;
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
    public class ClosePurchaseOrderCommandHandler
        : IRequestHandler<ClosePurchaseOrderCommand, ServiceResponse<bool>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<ClosePurchaseOrderCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public ClosePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<ClosePurchaseOrderCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(ClosePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.FindAsync(request.Id);

            if (purchaseOrder == null)
            {
                _logger.LogError("Purchase order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            purchaseOrder.IsClosed = true;
            purchaseOrder.ClosedDate = DateTime.Now;

            _purchaseOrderRepository.Update(purchaseOrder);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while closing Delivery Schedule.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
