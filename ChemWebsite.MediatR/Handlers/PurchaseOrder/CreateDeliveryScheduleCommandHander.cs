using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class CreateDeliveryScheduleCommandHander : IRequestHandler<CreateDeliveryScheduleCommand, ServiceResponse<PurchaseOrderDeliveryScheduleDto>>
    {
        private readonly IPurchaseOrderDeliveryScheduleRepository _poDeliveryScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDeliveryScheduleCommandHander> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public CreateDeliveryScheduleCommandHander(
            IPurchaseOrderDeliveryScheduleRepository poDeliveryScheduleRepository,
            IMapper mapper,
            ILogger<CreateDeliveryScheduleCommandHander> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPurchaseOrderRepository purchaseOrderRepository)
        {
            _poDeliveryScheduleRepository = poDeliveryScheduleRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<ServiceResponse<PurchaseOrderDeliveryScheduleDto>> Handle(CreateDeliveryScheduleCommand request, CancellationToken cancellationToken)
        {
            var po = await _purchaseOrderRepository.FindAsync(request.PurchaseOrderId);
            if (po == null)
            {
                _logger.LogError("Purchase order not found", request);
                return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.Return404("Purchase order not found");
            }

            var deliverySchedule = _mapper.Map<PurchaseOrderDeliverySchedule>(request);
            _poDeliveryScheduleRepository.Add(deliverySchedule);

            if (request.IsReceived)
            {
                po.AvailableQuantity = po.AvailableQuantity + request.ActualDispatchQuantity ?? 0;
                if (po.AvailableQuantity == po.TotalQuantity)
                {
                    po.IsClosed = true;
                    po.ClosedDate = DateTime.Now;
                }
                _purchaseOrderRepository.Update(po);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while creating Delivery Schedule.");
                return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.Return500();
            }
            return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.ReturnResultWith201(_mapper.Map<PurchaseOrderDeliveryScheduleDto>(deliverySchedule));
        }
    }
}
