using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{

    public class DeletePurchaseOrderCommandHandler
       : IRequestHandler<DeletePurchaseOrderCommand, ServiceResponse<bool>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly ILogger<DeletePurchaseOrderCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;

        public DeletePurchaseOrderCommandHandler(IPurchaseOrderRepository purchaseOrderRepository,
            ILogger<DeletePurchaseOrderCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _logger = logger;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _purchaseOrderRepository.FindAsync(request.Id);

            if (purchaseOrder == null)
            {
                _logger.LogError("Purchase order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            purchaseOrder.IsDeleted = true;

            _purchaseOrderRepository.Update(purchaseOrder);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting Purchase order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
