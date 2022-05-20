using AutoMapper;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetPurchaseOrderDeliveryScheduleQueryHandler
        : IRequestHandler<GetPurchaseOrderDeliveryScheduleQuery, List<PurchaseOrderDeliveryScheduleDto>>
    {

        private readonly IPurchaseOrderDeliveryScheduleRepository _purchaseOrderDelieveryScheduleRepository;
        private readonly IMapper _mapper;

        public GetPurchaseOrderDeliveryScheduleQueryHandler(
            IPurchaseOrderDeliveryScheduleRepository purchaseOrderDelieveryScheduleRepository,
            IMapper mapper)
        {
            _purchaseOrderDelieveryScheduleRepository = purchaseOrderDelieveryScheduleRepository;
            _mapper = mapper;
        }

        public async Task<List<PurchaseOrderDeliveryScheduleDto>> Handle(GetPurchaseOrderDeliveryScheduleQuery request, CancellationToken cancellationToken)
        {
            var poSchedules = await _purchaseOrderDelieveryScheduleRepository.All
                .Where(c => c.PurchaseOrderId == request.PurchaseOrderId)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<PurchaseOrderDeliveryScheduleDto>>(poSchedules);
        }
    }
}
