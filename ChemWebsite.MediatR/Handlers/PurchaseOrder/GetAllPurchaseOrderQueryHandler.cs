using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllPurchaseOrderQueryHandler : IRequestHandler<GetAllPurchaseOrderQuery, PurchaseOrderList>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetAllPurchaseOrderQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public Task<PurchaseOrderList> Handle(GetAllPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            return _purchaseOrderRepository.GetAllPurchaseOrders(request.PurchaseOrderResource);
        }
    }
}
