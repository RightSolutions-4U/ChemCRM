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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers.SupplierChemical
{
    public class AddSupplierChemicalCommandHandler : IRequestHandler<AddSupplierChemicalCommand, ServiceResponse<SupplierDto>>
    {
        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddSupplierChemicalCommandHandler> _logger;

        public AddSupplierChemicalCommandHandler(
            ISupplierChemicalRepository supplierChemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddSupplierChemicalCommandHandler> logger)
        {
            _supplierChemicalRepository = supplierChemicalRepository;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<SupplierDto>> Handle(AddSupplierChemicalCommand request, CancellationToken cancellationToken)
        {
            var isSupplierChemicalAdded = false;
            foreach (Guid chemicalId in request.ChemicalIdList.Distinct())
            {
                var chemicalSupplier = await _supplierChemicalRepository.FindBy(c => c.SupplierId == request.SupplierId && c.ChemicalId == chemicalId).FirstOrDefaultAsync();
                if (chemicalSupplier == null)
                {
                    isSupplierChemicalAdded = true;
                    chemicalSupplier = new ChemicalSupplier();
                    chemicalSupplier.SupplierId = request.SupplierId;
                    chemicalSupplier.ChemicalId = chemicalId;
                    _supplierChemicalRepository.Add(chemicalSupplier);
                }
            }

            if (isSupplierChemicalAdded && await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving supplier Chemicals.");
                return ServiceResponse<SupplierDto>.Return500();
            }
            return ServiceResponse<SupplierDto>.ReturnSuccess();
        }
    }
}
