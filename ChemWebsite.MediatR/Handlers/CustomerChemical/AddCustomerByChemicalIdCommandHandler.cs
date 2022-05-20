using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddCustomerByChemicalIdCommandHandler : IRequestHandler<AddCustomerByChemicalIdCommand, bool>
    {
        private readonly IChemicalCustomerRepository _chemicalCustomerRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddCustomerByChemicalIdCommandHandler> _logger;

        public AddCustomerByChemicalIdCommandHandler(
            IChemicalCustomerRepository chemicalCustomerRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddCustomerByChemicalIdCommandHandler> logger)
        {
            _chemicalCustomerRepository = chemicalCustomerRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(AddCustomerByChemicalIdCommand request, CancellationToken cancellationToken)
        {
            var existEntity = await _chemicalCustomerRepository
              .FindBy(c => c.CustomerId == request.CustomerId && c.ChemicalId == request.ChemicalId)
              .FirstOrDefaultAsync();
            if (existEntity != null)
            {
                return false;
            }
            var chemicalCustomer = new ChemicalCustomer
            {
                ChemicalId = request.ChemicalId,
                CustomerId = request.CustomerId
            };
            _chemicalCustomerRepository.Add(chemicalCustomer);

            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving Customer Chemicals.");
                return false;
            }
            return true;
        }
    }
}
