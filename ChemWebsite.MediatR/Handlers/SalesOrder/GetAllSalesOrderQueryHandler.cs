using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllSalesOrderQueryHandler : IRequestHandler<GetAllSalesOrderQuery, SalesOrderList>
    {
        private readonly ISalesOrderRepository _salesOrderRepository;

        public GetAllSalesOrderQueryHandler(
            ISalesOrderRepository salesOrderRepository
            )
        {
            _salesOrderRepository = salesOrderRepository;
        }

        public async Task<SalesOrderList> Handle(GetAllSalesOrderQuery request, CancellationToken cancellationToken)
        {
            return await _salesOrderRepository.GetSalesOrders(request.SaleOrderResource);
        }
    }
}
