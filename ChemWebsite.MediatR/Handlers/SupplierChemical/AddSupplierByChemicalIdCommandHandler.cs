using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;
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
    public class AddSupplierByChemicalIdCommandHandler : IRequestHandler<AddSupplierByChemicalIdCommand, bool>
    {

        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly ILogger<AddSupplierByChemicalIdCommandHandler> _logger;

        public AddSupplierByChemicalIdCommandHandler(
            ISupplierChemicalRepository supplierChemicalRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            ILogger<AddSupplierByChemicalIdCommandHandler> logger)
        {
            _supplierChemicalRepository = supplierChemicalRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<bool> Handle(AddSupplierByChemicalIdCommand request, CancellationToken cancellationToken)
        {
            var existEntity = await _supplierChemicalRepository
                .FindBy(c => c.SupplierId == request.SupplierId && c.ChemicalId == request.ChemicalId)
                .FirstOrDefaultAsync();
            if (existEntity != null)
            {
                return false;
            }
            var chemicalSupplier = new ChemicalSupplier
            {
                ChemicalId = request.ChemicalId,
                SupplierId = request.SupplierId
            };
            _supplierChemicalRepository.Add(chemicalSupplier);


            if (await _uow.SaveAsync() <= 0)
            {
                _logger.LogError("Error While saving supplier Chemicals.");
                return false;
            }
            return true;
        }
    }
}
