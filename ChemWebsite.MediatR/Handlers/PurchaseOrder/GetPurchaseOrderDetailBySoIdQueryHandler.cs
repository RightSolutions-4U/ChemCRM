using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetPurchaseOrderDetailBySoIdQueryHandler : IRequestHandler<GetPurchaseOrderDetailBySoIdQuery, ServiceResponse<List<PurchaseOrderShort>>>
    {
        private readonly ISalesPurchaseOrderItemRepository _salesPurchaseOrderItemRepository;

        public GetPurchaseOrderDetailBySoIdQueryHandler(ISalesPurchaseOrderItemRepository salesPurchaseOrderItemRepository)
        {
            _salesPurchaseOrderItemRepository = salesPurchaseOrderItemRepository;
        }
        public async Task<ServiceResponse<List<PurchaseOrderShort>>> Handle(GetPurchaseOrderDetailBySoIdQuery request, CancellationToken cancellationToken)
        {
            var entities = await _salesPurchaseOrderItemRepository.All
                 .Include(c => c.PurchaseOrder)
                    .ThenInclude(c => c.Supplier)
                .Where(c => c.SalesOrderId == request.SalesOrderId)
                .Select(c => new PurchaseOrderShort
                {
                    PurchaseOrderId = c.PurchaseOrderId,
                    PurchaseOrderName = c.PurchaseOrder.OrderNumber,
                    Quantity = c.Quantity,
                    SupplierId = c.PurchaseOrder.SupplierId,
                    SupplierName = c.PurchaseOrder.Supplier.SupplierName
                })
                .ToListAsync();
            return ServiceResponse<List<PurchaseOrderShort>>.ReturnResultWith200(entities);
        }
    }
}
