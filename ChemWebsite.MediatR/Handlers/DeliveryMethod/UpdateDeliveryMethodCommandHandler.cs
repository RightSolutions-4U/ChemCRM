using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
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
   public class UpdateDeliveryMethodCommandHandler : IRequestHandler<UpdateDeliveryMethodCommand, ServiceResponse<bool>>
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<UpdateDeliveryMethodCommandHandler> _logger;
        private readonly IMapper _mapper;
        public UpdateDeliveryMethodCommandHandler(
           IDeliveryMethodRepository deliveryMethodRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<UpdateDeliveryMethodCommandHandler> logger,
            IMapper mapper
            )
        {
            _deliveryMethodRepository = deliveryMethodRepository;
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<bool>> Handle(UpdateDeliveryMethodCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _deliveryMethodRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Deliver Method Already Exist for another Delivery Method.");
                return ServiceResponse<bool>.Return409("Deliver Method Already Exist for another Delivery Method.");
            }
            var entity = _mapper.Map<DeliveryMethod>(request);
            _deliveryMethodRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Deliver Method");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
