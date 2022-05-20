using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddDeliveryMethodCommandHandler : IRequestHandler<AddDeliveryMethodCommand, ServiceResponse<DeliveryMethodDto>>
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddDeliveryMethodCommandHandler> _logger;
        public AddDeliveryMethodCommandHandler(
           IDeliveryMethodRepository deliveryMethodRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddDeliveryMethodCommandHandler> logger
            )
        {
            _deliveryMethodRepository = deliveryMethodRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<DeliveryMethodDto>> Handle(AddDeliveryMethodCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _deliveryMethodRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Delivery Method Already Exist");
                return ServiceResponse<DeliveryMethodDto>.Return409("Delivery Method Already Exist.");
            }
            var entity = _mapper.Map<DeliveryMethod>(request);
            entity.Id = Guid.NewGuid();
            _deliveryMethodRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error While saving Delivery Method.");
                return ServiceResponse<DeliveryMethodDto>.Return500();
            }
            return ServiceResponse<DeliveryMethodDto>.ReturnResultWith200(_mapper.Map<DeliveryMethodDto>(entity));
        }
    }
}
