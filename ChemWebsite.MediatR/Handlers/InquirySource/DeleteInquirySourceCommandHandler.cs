using ChemWebsite.Common.UnitOfWork;
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
    public class DeleteInquirySourceCommandHandler 
        : IRequestHandler<DeleteInquirySourceCommand, ServiceResponse<bool>>
    {
        private readonly IInquirySourceRepository _inquirySourceRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteInquirySourceCommandHandler> _logger;
        public DeleteInquirySourceCommandHandler(
           IInquirySourceRepository inquirySourceRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteInquirySourceCommandHandler> logger
            )
        {
            _inquirySourceRepository = inquirySourceRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<bool>> Handle(DeleteInquirySourceCommand request, CancellationToken cancellationToken)
        {
            var existingEntity = await _inquirySourceRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (existingEntity == null)
            {
                _logger.LogError("Inquiry Source does not Exist");
                return ServiceResponse<bool>.Return409("Inquiry Source does not  Exist.");
            }
            _inquirySourceRepository.Remove(existingEntity);
            if (await _uow.SaveAsync() <= 0)
            {

                _logger.LogError("Error while saving Inquiry Source.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
