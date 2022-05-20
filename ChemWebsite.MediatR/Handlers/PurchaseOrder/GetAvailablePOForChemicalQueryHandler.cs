using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAvailablePOForChemicalQueryHandler : IRequestHandler<GetAvailablePOForChemicalQuery, List<PurchaseOrderDto>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public GetAvailablePOForChemicalQueryHandler(IPurchaseOrderRepository purchaseOrderRepository)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<List<PurchaseOrderDto>> Handle(GetAvailablePOForChemicalQuery request, CancellationToken cancellationToken)
        {
            var po = await _purchaseOrderRepository.All
                .Where(c => c.ChemicalId == request.ChemicalId && c.InStockQuantity > 0)
                .Select(c => new PurchaseOrderDto
                {
                    Id = c.Id,
                    OrderNumber = c.OrderNumber,
                    InStockQuantity = c.InStockQuantity,
                    SupplierName = c.Supplier.SupplierName,
                    PricePerUnit = c.PricePerUnit
                }).ToListAsync();
            return po;
        }
    }
}
