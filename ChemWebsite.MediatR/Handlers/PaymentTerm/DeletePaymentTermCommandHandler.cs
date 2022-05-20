using AutoMapper;
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
   public class DeletePaymentTermCommandHandler : IRequestHandler<DeletePaymentTermCommand, ServiceResponse<bool>>
    {
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePaymentTermCommandHandler> _logger;
        public DeletePaymentTermCommandHandler(
           IPaymentTermRepository paymentTermRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeletePaymentTermCommandHandler> logger
            )
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeletePaymentTermCommand request, CancellationToken cancellationToken)
        {
             var existingEntity = await _paymentTermRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (existingEntity == null)
            {
                _logger.LogError("Payment Term not  Exist.");
                return ServiceResponse<bool>.Return409("Payment Term not  Exist.");
            }
            _paymentTermRepository.Remove(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Save Payment Term have Error");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
