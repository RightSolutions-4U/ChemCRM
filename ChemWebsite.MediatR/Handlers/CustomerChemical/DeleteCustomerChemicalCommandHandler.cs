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

namespace ChemWebsite.MediatR.Handlers.CustomerChemical
{
    public class DeleteCustomerChemicalCommandHandler : IRequestHandler<DeleteCustomerChemicalCommand, ServiceResponse<bool>>
    {
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteCustomerChemicalCommand> _logger;

        public DeleteCustomerChemicalCommandHandler(IChemicalCustomerRepository chemicalCustomerRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<DeleteCustomerChemicalCommand> logger)
        {
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteCustomerChemicalCommand request, CancellationToken cancellationToken)
        {
            var customerChemical = await _chemicalCustomerRepository
                .FindBy(c => c.CustomerId == request.CustomerId && c.ChemicalId == request.ChemicalId)
                .FirstOrDefaultAsync();
            if (customerChemical == null)
            {
                return ServiceResponse<bool>.Return404();
            }

            _chemicalCustomerRepository.Remove(customerChemical);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error while Deleting Customer.");
                return ServiceResponse<bool>.Return500();
            }
            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
