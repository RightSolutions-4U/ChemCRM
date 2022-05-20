using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteInquiryCommandHandler : IRequestHandler<DeleteInquiryCommand, ServiceResponse<InquiryDto>>
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly ILogger<DeleteInquiryCommandHandler> _logger;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly DashboardStatics _dashboardStatics;
        public DeleteInquiryCommandHandler(IInquiryRepository inquiryRepository,
            ILogger<DeleteInquiryCommandHandler> logger,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            DashboardStatics dashboardStatics)
        {
            _inquiryRepository = inquiryRepository;
            _logger = logger;
            _uow = uow;
            _dashboardStatics = dashboardStatics;
        }
        public async Task<ServiceResponse<InquiryDto>> Handle(DeleteInquiryCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _inquiryRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                _logger.LogError("Inquiry does not exists.");
                return ServiceResponse<InquiryDto>.Return404();
            }
            entityExist.IsDeleted = true;
            _inquiryRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<InquiryDto>.Return500();
            }
            _dashboardStatics.InquiryCount = _dashboardStatics.InquiryCount - 1;
            return ServiceResponse<InquiryDto>.ReturnResultWith204();
        }
    }
}
