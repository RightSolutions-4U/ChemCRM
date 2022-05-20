using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetSupplierByChemicalQueryHandler : IRequestHandler<GetSupplierByChemicalQuery, SupplierListDto>
    {
        private readonly ISupplierChemicalRepository _supplierChemicalRepository;
        private readonly IMapper _mapper;

        public GetSupplierByChemicalQueryHandler(ISupplierChemicalRepository supplierChemicalRepository,
            IMapper mapper)
        {
            _supplierChemicalRepository = supplierChemicalRepository;
            _mapper = mapper;
        }
        public async Task<SupplierListDto> Handle(GetSupplierByChemicalQuery request, CancellationToken cancellationToken)
        {
            var supplierQuery = _supplierChemicalRepository
                .AllIncluding(c => c.Supplier, cs => cs.Supplier.SupplierEmails, a => a.Supplier.SupplierAddresses)
                .OrderBy(c => c.Supplier.SupplierName)
                .Where(c => !c.Supplier.IsDeleted
                    && c.ChemicalId == request.SupplierResource.ChemicalId);

            if (!string.IsNullOrWhiteSpace(request.SupplierResource.SupplierName))
            {
                var genreForWhereClause = request.SupplierResource.SupplierName.Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                supplierQuery = supplierQuery.Where(a => EF.Functions.Like(a.Supplier.SupplierName, $"{encodingName}%", @"\"));
            }

            if (!string.IsNullOrEmpty(request.SupplierResource.MobileNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = request.SupplierResource.MobileNo
                    .Trim().ToLowerInvariant();
                supplierQuery = supplierQuery
                    .Where(a => (a.Supplier.MobileNo != null && EF.Functions.Like(a.Supplier.MobileNo, $"%{searchQueryForWhereClause}%")) ||
                    (a.Supplier.PhoneNo != null && EF.Functions.Like(a.Supplier.PhoneNo, $"%{searchQueryForWhereClause}%")));
            }

            if (!string.IsNullOrEmpty(request.SupplierResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = request.SupplierResource.Email
                    .Trim().ToLowerInvariant();
                supplierQuery = supplierQuery
                    .Where(a => a.Supplier.SupplierEmails.Where(c => EF.Functions.Like(c.Email, $"{searchQueryForWhereClause}%")).Any());
            }

            if (!string.IsNullOrWhiteSpace(request.SupplierResource.Country))
            {
                supplierQuery = supplierQuery
                  .Where(a => a.Supplier.SupplierAddresses.Where(c => c.CountryName == request.SupplierResource.Country).Any());
            }

            var suppliers = await supplierQuery
                .Select(c => c.Supplier)
                .Skip(request.SupplierResource.Skip)
                .Take(request.SupplierResource.PageSize)
                .ToListAsync();

            var supplierDtos = suppliers.Select(c => new SupplierDto
            {
                Id = c.Id,
                Email = string.Join(", ", c.SupplierEmails.Select(c => c.Email)),
                MobileNo = c.MobileNo,
                Country = c.SupplierAddresses.FirstOrDefault()?.CountryName,
                SupplierName = c.SupplierName
            }).ToList();

            var result = new SupplierListDto
            {
                Suppliers = supplierDtos,
                TotalCount = supplierQuery.Count()
            };
            return result;
        }
    }
}
