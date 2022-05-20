using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllInventoryQueryHandler : IRequestHandler<GetAllInventoryQuery, InventoryList>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetAllInventoryQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<InventoryList> Handle(GetAllInventoryQuery request, CancellationToken cancellationToken)
        {
            var query = _purchaseOrderRepository.All
                             .Include(c => c.Chemical)
                             .OrderBy(c => c.Chemical.Name)
                             .Where(c => c.InStockQuantity > 0);

            if (!string.IsNullOrEmpty(request.ChemicalName))
            {
                var chemicalName = request.ChemicalName.Trim();
                query = query.Where(a => EF.Functions.Like(a.Chemical.Name, $"%{chemicalName}%"));
            }
            if (!string.IsNullOrEmpty(request.CasNo))
            {
                var casNo = request.CasNo.Trim();
                query = query.Where(a => EF.Functions.Like(a.Chemical.CasNumber, $"{casNo}%"));
            }

            return await new InventoryList().Create(query,
              request.Skip,
              request.PageSize);

        }
    }
}
