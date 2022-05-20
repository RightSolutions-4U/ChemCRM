using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class IsEmailOrPhoneExistQueryHandler : IRequestHandler<IsEmailOrPhoneExistQuery, Boolean>
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public IsEmailOrPhoneExistQueryHandler(ISupplierRepository supplierRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _supplierRepository = supplierRepository;
            _uow = uow;
        }

        public async Task<bool> Handle(IsEmailOrPhoneExistQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(request.EMail))
            {
                return await _uow.Context.SupplierEmails.AnyAsync(c => c.Email == request.EMail && c.Id != request.Id);
            }
            else
            {
                return await _supplierRepository.All.AnyAsync(c => c.MobileNo == request.Phone && c.Id != request.Id);
            }
        }
    }
}
