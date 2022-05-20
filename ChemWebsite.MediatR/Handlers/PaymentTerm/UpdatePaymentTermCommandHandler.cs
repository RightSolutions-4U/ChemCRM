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
    public class UpdatePaymentTermCommandHandler : IRequestHandler<UpdatePaymentTermCommand, ServiceResponse<bool>>
    {
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePaymentTermCommandHandler> _logger;
        public UpdatePaymentTermCommandHandler(
           IPaymentTermRepository paymentTermRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<UpdatePaymentTermCommandHandler> logger
            )
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdatePaymentTermCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _paymentTermRepository.FindBy(c => c.Name == request.Name && c.Id != request.Id).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Payment Term Already Exist for another Delivery Method.");
                return ServiceResponse<bool>.Return409("Payment Term Already Exist for another Payment Term.");
            }
            var entity = _mapper.Map<PaymentTerm>(request);
            _paymentTermRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Save Page have Error");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
