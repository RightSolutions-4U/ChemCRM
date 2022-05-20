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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
   public class AddPaymentTermCommandHandler : IRequestHandler<AddPaymentTermCommand, ServiceResponse<PaymentTermDto>>
    {
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddPaymentTermCommandHandler> _logger;
        public AddPaymentTermCommandHandler(
           IPaymentTermRepository paymentTermRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddPaymentTermCommandHandler> logger
            )
        {
            _paymentTermRepository = paymentTermRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<PaymentTermDto>> Handle(AddPaymentTermCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _paymentTermRepository.FindBy(c => c.Name == request.Name).FirstOrDefaultAsync();
            if (existingEntity != null)
            {
                _logger.LogError("Page Already Exist");
                return ServiceResponse<PaymentTermDto>.Return409("Delivery Method Already Exist.");
            }
            var entity = _mapper.Map<PaymentTerm>(request);
            entity.Id = Guid.NewGuid();
            _paymentTermRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Save Page have Error");
                return ServiceResponse<PaymentTermDto>.Return500();
            }
            return ServiceResponse<PaymentTermDto>.ReturnResultWith200(_mapper.Map<PaymentTermDto>(entity));
        }
    }
}