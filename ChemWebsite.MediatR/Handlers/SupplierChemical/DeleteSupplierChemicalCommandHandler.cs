using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteSupplierChemicalCommandHandler : IRequestHandler<DeleteSupplierChemicalCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<DeleteSupplierChemicalCommandHandler> _logger;

        public DeleteSupplierChemicalCommandHandler(
           ISupplierChemicalRepository supplierChemicalRepository,
           IUnitOfWork<ChemWebsiteDbContext> uow,
           ILogger<DeleteSupplierChemicalCommandHandler> logger)
        {
            _supplierChemicalRepository = supplierChemicalRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<SupplierDto>> Handle(DeleteSupplierChemicalCommand request, CancellationToken cancellationToken)
        {
            var chemicalSupplier = await _supplierChemicalRepository.All
                .Where(c => c.SupplierId == request.SupplierId && c.ChemicalId == request.ChemicalId)
                .FirstOrDefaultAsync();

            if (chemicalSupplier == null)
            {
                _logger.LogError("Supplier Product does not exists");
                return ServiceResponse<SupplierDto>.Return404();
            }

            _supplierChemicalRepository.Remove(chemicalSupplier);
            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving supplier Product.");
                return ServiceResponse<SupplierDto>.Return500();
            }
            return ServiceResponse<SupplierDto>.ReturnSuccess();
        }
    }
}
