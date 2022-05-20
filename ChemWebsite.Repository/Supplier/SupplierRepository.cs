using AutoMapper;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class SupplierRepository : GenericRepository<Supplier, ChemWebsiteDbContext>, ISupplierRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        public SupplierRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
             IMapper mapper)
            : base(uow)
        {
            _mapper = mapper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<SupplierList> GetSuppliers(SupplierResource supplierResource)
        {
            var collectionBeforePaging =
                All.ApplySort(supplierResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<SupplierDto, Supplier>());
            collectionBeforePaging = collectionBeforePaging.Include(c => c.SupplierEmails).Include(c => c.SupplierAddresses);
            if (!string.IsNullOrEmpty(supplierResource.SupplierName))
            {
                // trim & ignore casing
                var genreForWhereClause = supplierResource.SupplierName
                    .Trim().ToLowerInvariant();
                var name = Uri.UnescapeDataString(genreForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.SupplierName, $"{encodingName}%"));
            }

            if (!string.IsNullOrEmpty(supplierResource.MobileNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.MobileNo
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => (a.MobileNo != null && EF.Functions.Like(a.MobileNo, $"%{searchQueryForWhereClause}%")) ||
                    (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"%{searchQueryForWhereClause}%")));
            }
            if (!string.IsNullOrEmpty(supplierResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.Email
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.SupplierEmails.Where(c => EF.Functions.Like(c.Email, $"{searchQueryForWhereClause}%")).Any());
            }
            if (!string.IsNullOrEmpty(supplierResource.Website))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = supplierResource.Website
                    .Trim().ToLowerInvariant();

                var name = Uri.UnescapeDataString(searchQueryForWhereClause);
                var encodingName = WebUtility.UrlDecode(name);
                var ecapestring = Regex.Unescape(encodingName);
                encodingName = encodingName.Replace(@"\", @"\\").Replace("%", @"\%").Replace("_", @"\_").Replace("[", @"\[").Replace(" ", "%");
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.Website, $"%{encodingName}%"));
            }
            if (!string.IsNullOrEmpty(supplierResource.SearchQuery))
            {
                if (ValidateEmail.IsValid(supplierResource.SearchQuery))
                {
                    var searchQueryForWhereClause = supplierResource.SearchQuery
                  .Trim().ToLowerInvariant();
                    collectionBeforePaging = collectionBeforePaging
                  .Where(a => a.SupplierEmails.Where(c => c.Email == searchQueryForWhereClause).Any());
                }
                else
                {
                    var searchQueryForWhereClause = supplierResource.SearchQuery
                  .Trim().ToLowerInvariant();
                    collectionBeforePaging = collectionBeforePaging
                        .Where(a =>
                        EF.Functions.Like(a.SupplierName, $"%{searchQueryForWhereClause}%")
                        || EF.Functions.Like(a.MobileNo, $"{searchQueryForWhereClause}%")
                        || (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"))
                        || EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%")
                        );
                }
            }

            if (!string.IsNullOrWhiteSpace(supplierResource.Country))
            {
                collectionBeforePaging = collectionBeforePaging
                  .Where(a => a.SupplierAddresses.Where(c => c.CountryName == supplierResource.Country).Any());
            }
            var SupplierList = new SupplierList(_mapper);
            return await SupplierList.Create(collectionBeforePaging, supplierResource.Skip, supplierResource.PageSize);
        }
    }
}
