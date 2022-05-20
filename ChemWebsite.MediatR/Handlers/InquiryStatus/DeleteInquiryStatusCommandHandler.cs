using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteInquiryStatusCommandHandler : IRequestHandler<DeleteInquiryStatusCommand, ServiceResponse<bool>>
    {
        private readonly IInquiryStatusRepository _inquiryStatusRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteInquiryStatusCommandHandler> _logger;

        public DeleteInquiryStatusCommandHandler(
           IInquiryStatusRepository inquiryStatusRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteInquiryStatusCommandHandler> logger
            )
        {
            _inquiryStatusRepository = inquiryStatusRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteInquiryStatusCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _inquiryStatusRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Inquiry Status Does not exists");
                return ServiceResponse<bool>.Return404("Inquiry Status Does not exists");
            }

            _inquiryStatusRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving Inquiry Status.");
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
