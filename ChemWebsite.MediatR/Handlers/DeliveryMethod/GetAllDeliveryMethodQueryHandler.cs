using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllDeliveryMethodQueryHandler : IRequestHandler<GetAllDeliveryMethodQuery, ServiceResponse<List<DeliveryMethodDto>>>
    {
        private readonly IDeliveryMethodRepository _deliveryMethodRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllDeliveryMethodQueryHandler> _logger;

        public GetAllDeliveryMethodQueryHandler(
            IDeliveryMethodRepository deliveryMethodRepository,
            IMapper mapper,
            ILogger<GetAllDeliveryMethodQueryHandler> logger)
        {
            _deliveryMethodRepository = deliveryMethodRepository;
            _mapper = mapper;
            _logger = logger;

        }
        public async Task<ServiceResponse<List<DeliveryMethodDto>>> Handle(GetAllDeliveryMethodQuery request, CancellationToken cancellationToken)
        {
            var entities = await _deliveryMethodRepository.All.ToListAsync();
            if (entities != null)
            {
                var dtoEntities = _mapper.Map<List<DeliveryMethodDto>>(entities);
                return ServiceResponse<List<DeliveryMethodDto>>.ReturnResultWith200(dtoEntities);
            }
            else
            {
                return ServiceResponse<List<DeliveryMethodDto>>.ReturnResultWith200(new List<DeliveryMethodDto>());
            }
        }
    }
}