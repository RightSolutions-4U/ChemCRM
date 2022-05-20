using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetPurchaseOrderQueryHandler : IRequestHandler<GetPurchaseOrderQuery, ServiceResponse<PurchaseOrderDto>>
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        private readonly IMapper _mapper;

        public GetPurchaseOrderQueryHandler(IPurchaseOrderRepository purchaseOrderRepository,
            IMapper mapper)
        {
            _purchaseOrderRepository = purchaseOrderRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<PurchaseOrderDto>> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            var entity = await _purchaseOrderRepository.All
                .Include(c => c.Chemical)
                .Include(c => c.Supplier)
                .Include(c => c.PackagingType)
                .Include(c => c.PurchaseOrderDeliverySchedules)
                .Include(c => c.PurchaseOrderAttachments)
                .Include(c => c.SalesPurchaseOrderItems)
                    .ThenInclude(d => d.SalesOrder)
                        .ThenInclude(c => c.Customer)
                .Where(c => c.Id == request.Id)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<PurchaseOrderDto>(entity);
            return ServiceResponse<PurchaseOrderDto>.ReturnResultWith200(dto);
        }
    }
}
