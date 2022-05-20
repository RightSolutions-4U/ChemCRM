using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteDeliveryMethodCommandHandler : IRequestHandler<DeleteDeliveryMethodCommand, ServiceResponse<bool>>
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteDeliveryMethodCommandHandler> _logger;
        public DeleteDeliveryMethodCommandHandler(
           IDeliveryMethodRepository deliveryMethodRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteDeliveryMethodCommandHandler> logger
            )
        {
            _deliveryMethodRepository = deliveryMethodRepository;

            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteDeliveryMethodCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _deliveryMethodRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (existingEntity == null)
            {
                _logger.LogError("Delivery Method does not Exist");
                return ServiceResponse<bool>.Return409("Delivery Method does not  Exist.");
            }
            _deliveryMethodRepository.Remove(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Delivery Method.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
