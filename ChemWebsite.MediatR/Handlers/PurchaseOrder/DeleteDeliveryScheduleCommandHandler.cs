using ChemWebsite.Common.UnitOfWork;
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
    public class DeleteDeliveryScheduleCommandHandler
        : IRequestHandler<DeleteDeliveryScheduleCommand, ServiceResponse<bool>>
    {
        private readonly IPurchaseOrderDeliveryScheduleRepository _poDeliveryScheduleRepository;
        private readonly ILogger<DeleteDeliveryScheduleCommand> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;


        public DeleteDeliveryScheduleCommandHandler(
            IPurchaseOrderDeliveryScheduleRepository poDeliveryScheduleRepository,
            ILogger<DeleteDeliveryScheduleCommand> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow)
        {
            _poDeliveryScheduleRepository = poDeliveryScheduleRepository;
            _logger = logger;
            _uow = uow;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteDeliveryScheduleCommand request, CancellationToken cancellationToken)
        {
            var poDeliverySchedule = await _poDeliveryScheduleRepository.FindAsync(request.Id);

            if (poDeliverySchedule == null)
            {
                _logger.LogError("Delivery Schedule does not exists.");
                return ServiceResponse<bool>.Return404();
            }

            _poDeliveryScheduleRepository.Remove(poDeliverySchedule);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting Delivery Schedule.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
