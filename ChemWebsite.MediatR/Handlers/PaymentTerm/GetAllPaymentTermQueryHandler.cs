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
   public class GetAllPaymentTermQueryHandler : IRequestHandler<GetAllPaymentTermQuery, ServiceResponse<List<PaymentTermDto>>>
    {
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllPaymentTermQueryHandler> _logger;
        public GetAllPaymentTermQueryHandler(
           IPaymentTermRepository paymentTermRepository,
            IMapper mapper,
            ILogger<GetAllPaymentTermQueryHandler> logger
            )
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<List<PaymentTermDto>>> Handle(GetAllPaymentTermQuery request, CancellationToken cancellationToken)
        {
            var entities = await _paymentTermRepository.All.ToListAsync();
            if (entities != null)
            {
                var dtoEntities = _mapper.Map<List<PaymentTermDto>>(entities);
                return ServiceResponse<List<PaymentTermDto>>.ReturnResultWith200(dtoEntities);
            }
            else
            {
                return ServiceResponse<List<PaymentTermDto>>.ReturnResultWith200(new List<PaymentTermDto>());
            }
        }
    }
}