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
    public class GetPaymentTermQueryHandler : IRequestHandler<GetPaymentTermQuery, ServiceResponse<PaymentTermDto>>
    {
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPaymentTermQueryHandler> _logger;
        public GetPaymentTermQueryHandler(
           IPaymentTermRepository paymentTermRepository,
            IMapper mapper,
            ILogger<GetPaymentTermQueryHandler> logger
            )
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ServiceResponse<PaymentTermDto>> Handle(GetPaymentTermQuery request, CancellationToken cancellationToken)
        {
            var entity = await _paymentTermRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<PaymentTermDto>.ReturnResultWith200(_mapper.Map<PaymentTermDto>(entity));
            else
            {
                _logger.LogError("Not found");
                return ServiceResponse<PaymentTermDto>.Return404();
            }
        }
    }
}