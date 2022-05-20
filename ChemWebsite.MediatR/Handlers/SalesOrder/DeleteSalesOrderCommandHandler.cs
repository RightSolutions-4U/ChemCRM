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
    public class DeleteSalesOrderCommandHandler
        : IRequestHandler<DeleteSalesOrderCommand, ServiceResponse<bool>>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteSalesOrderCommandHandler> _logger;

        public DeleteSalesOrderCommandHandler(ISalesOrderRepository salesOrderRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteSalesOrderCommandHandler> logger)
        {
            _salesOrderRepository = salesOrderRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteSalesOrderCommand request, CancellationToken cancellationToken)
        {
            var salesOrder = await _salesOrderRepository
                .All
                .Include(c => c.SalesPurchaseOrderItems)
                    .ThenInclude(c => c.PurchaseOrder)
                .FirstOrDefaultAsync(c => c.Id == request.Id);
            if (salesOrder == null)
            {
                _logger.LogError("Sales order does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            salesOrder.IsDeleted = true;

            foreach (var item in salesOrder.SalesPurchaseOrderItems)
            {
                item.PurchaseOrder.InStockQuantity = item.PurchaseOrder.InStockQuantity + item.Quantity;
            }

            _salesOrderRepository.Update(salesOrder);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting Sales Order.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
