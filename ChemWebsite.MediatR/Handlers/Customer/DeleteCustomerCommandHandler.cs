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
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ServiceResponse<bool>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly DashboardStatics _dashboardStatics;

        public DeleteCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteCustomerCommandHandler> logger,
            DashboardStatics dashboardStatics)
        {
            _customerRepository = customerRepository;
            _uow = uow;
            _logger = logger;
            _dashboardStatics = dashboardStatics;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _customerRepository.FindAsync(request.Id);
            if (entity == null)
            {
                _logger.LogError("Customer not found");
                return ServiceResponse<bool>.Return404();
            }
            _customerRepository.Delete(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while deleting the Customer.", request.Id);
                return ServiceResponse<bool>.Return500();
            }
            _dashboardStatics.CustomerCount = _dashboardStatics.CustomerCount - 1 ;
            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
