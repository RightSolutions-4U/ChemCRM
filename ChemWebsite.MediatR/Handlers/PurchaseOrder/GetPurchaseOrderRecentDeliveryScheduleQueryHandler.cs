using AutoMapper;
using ChemWebsite.Data.Entities;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetPurchaseOrderRecentDeliveryScheduleQueryHandler : IRequestHandler<GetPurchaseOrderRecentDeliveryScheduleQuery, List<PurchaseOrderRecentDeliverySchedule>>
    {

        private readonly IPurchaseOrderDeliveryScheduleRepository _purchaseOrderDeliveryScheduleRepository;
        private readonly ILogger<GetPurchaseOrderRecentDeliveryScheduleQueryHandler> _logger;


        public GetPurchaseOrderRecentDeliveryScheduleQueryHandler(
            IPurchaseOrderDeliveryScheduleRepository purchaseOrderDeliveryScheduleRepository,
            IMapper mapper,
            ILogger<GetPurchaseOrderRecentDeliveryScheduleQueryHandler> logger
          )
        {
            _purchaseOrderDeliveryScheduleRepository = purchaseOrderDeliveryScheduleRepository;
            _logger = logger;

        }

        public async Task<List<PurchaseOrderRecentDeliverySchedule>> Handle(GetPurchaseOrderRecentDeliveryScheduleQuery request, CancellationToken cancellationToken)
        {
            var entities = await _purchaseOrderDeliveryScheduleRepository.All
                            .Include(c => c.PurchaseOrder)
                             .ThenInclude(c => c.Supplier)
                            .Include(c => c.PurchaseOrder)
                              .ThenInclude(c => c.Chemical)
                             .Where(c => !c.IsReceived)
                             .OrderByDescending(c => c.ExpectedDispatchDate)
                             .Take(10)
                             .Select(c => new PurchaseOrderRecentDeliverySchedule
                             {
                                 PurchaseOrderId = c.PurchaseOrderId,
                                 PurchaseOrderNumber = c.PurchaseOrder.OrderNumber,
                                 ExpectedDispatchQuantity = c.ExpectedDispatchQuantity,
                                 ExpectedDispatchDate = c.ExpectedDispatchDate,
                                 SupplierId = c.PurchaseOrder.SupplierId,
                                 SupplierName = c.PurchaseOrder.Supplier.SupplierName,
                                 ChemicalId = c.PurchaseOrder.ChemicalId,
                                 ChemicalName = c.PurchaseOrder.Chemical.Name
                             })
                         .ToListAsync();

            return entities;
        }
    }
}
