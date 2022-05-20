using AutoMapper;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class CustomerRepository : GenericRepository<Customer, ChemWebsiteDbContext>, ICustomerRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IMapper _mapper;
        public CustomerRepository(IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IMapper mapper)
            : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _mapper = mapper;
        }

        public async Task<CustomerList> GetCustomers(CustomerResource customerResource)
        {
            var collectionBeforePaging =
                All.ApplySort(customerResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<CustomerDto, Customer>());

            if (!string.IsNullOrEmpty(customerResource.CustomerName))
            {
                // trim & ignore casing
                var genreForWhereClause = customerResource.CustomerName
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.CustomerName, $"{genreForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(customerResource.ContactPerson))
            {
                // trim & ignore casing
                var genreForWhereClause = customerResource.ContactPerson
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.ContactPerson, $"{genreForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(customerResource.PhoneNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = customerResource.PhoneNo
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(customerResource.MobileNo))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = customerResource.MobileNo
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.MobileNo != null && EF.Functions.Like(a.MobileNo, $"{searchQueryForWhereClause}%"));
            }
            if (!string.IsNullOrEmpty(customerResource.Email))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = customerResource.Email
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Email != null && EF.Functions.Like(a.Email, $"{searchQueryForWhereClause}%"));
            }

            if (!string.IsNullOrEmpty(customerResource.SearchQuery))
            {
                // trim & ignore casing
                var searchQueryForWhereClause = customerResource.SearchQuery
                    .Trim().ToLowerInvariant();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => (a.Email != null && EF.Functions.Like(a.Email, $"{searchQueryForWhereClause}%"))
                    || EF.Functions.Like(a.CustomerName, $"%{searchQueryForWhereClause}%")
                    || EF.Functions.Like(a.MobileNo, $"{searchQueryForWhereClause}%")
                    || (a.PhoneNo != null && EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%"))
                    || EF.Functions.Like(a.PhoneNo, $"{searchQueryForWhereClause}%")
                    );
            }

            var CustomerList = new CustomerList(_mapper);
            return await CustomerList.Create(collectionBeforePaging,
                customerResource.Skip,
                customerResource.PageSize);
        }
    }
}
