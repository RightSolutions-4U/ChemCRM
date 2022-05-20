using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
   public class GetDeliveryMethodQueryHandler : IRequestHandler<GetDeliveryMethodQuery, ServiceResponse<DeliveryMethodDto>>
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDeliveryMethodQueryHandler> _logger;

        public GetDeliveryMethodQueryHandler(
            IDeliveryMethodRepository deliveryMethodRepository,
            IMapper mapper,
            ILogger<GetDeliveryMethodQueryHandler> logger)
        {
            _deliveryMethodRepository = deliveryMethodRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<ServiceResponse<DeliveryMethodDto>> Handle(GetDeliveryMethodQuery request, CancellationToken cancellationToken)
        {
            var entity = await _deliveryMethodRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<DeliveryMethodDto>.ReturnResultWith200(_mapper.Map<DeliveryMethodDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<DeliveryMethodDto>.Return404();
            }
        }
    }
}