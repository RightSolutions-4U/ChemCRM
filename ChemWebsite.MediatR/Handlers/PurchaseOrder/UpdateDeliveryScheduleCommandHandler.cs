using Amazon.Runtime.Internal.Util;
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
    public class UpdateDeliveryScheduleCommandHandler
        : IRequestHandler<UpdateDeliveryScheduleCommand, ServiceResponse<PurchaseOrderDeliveryScheduleDto>>
    {
        private readonly IPurchaseOrderDeliveryScheduleRepository _poDeliveryScheduleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateDeliveryScheduleCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;

        public UpdateDeliveryScheduleCommandHandler(
            IPurchaseOrderDeliveryScheduleRepository poDeliveryScheduleRepository,
            IMapper mapper,
            ILogger<UpdateDeliveryScheduleCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPurchaseOrderRepository purchaseOrderRepository)
        {
            _poDeliveryScheduleRepository = poDeliveryScheduleRepository;
            _mapper = mapper;
            _logger = logger;
            _uow = uow;
            _purchaseOrderRepository = purchaseOrderRepository;
        }

        public async Task<ServiceResponse<PurchaseOrderDeliveryScheduleDto>> Handle(UpdateDeliveryScheduleCommand request, CancellationToken cancellationToken)
        {
            var poDeliverySchedule = await _poDeliveryScheduleRepository.FindAsync(request.Id);
            if (poDeliverySchedule == null)
            {
                _logger.LogError("Delivery Schedule does not exists.");
                return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.Return404();
            }

            var poToUpdate = _mapper.Map(request, poDeliverySchedule);
            _poDeliveryScheduleRepository.Update(poToUpdate);


            if (request.IsReceived)
            {
                var po = await _purchaseOrderRepository.FindAsync(request.PurchaseOrderId);
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
                _logger.LogError("Error while updating Delivery Schedule.");
                return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.Return500();
            }
            return ServiceResponse<PurchaseOrderDeliveryScheduleDto>.ReturnResultWith200(_mapper.Map<PurchaseOrderDeliveryScheduleDto>(poToUpdate));
        }
    }
}
