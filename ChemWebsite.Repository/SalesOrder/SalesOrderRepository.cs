using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class SalesOrderRepository : GenericRepository<SalesOrder, ChemWebsiteDbContext>,
           ISalesOrderRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public SalesOrderRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService
            )
            : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<SalesOrderList> GetSalesOrders(SaleOrderResource saleOrderResource)
        {
            var collectionBeforePaging =
                All.ApplySort(saleOrderResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<SalesOrderListDto, SalesOrder>());
            collectionBeforePaging = collectionBeforePaging
                .Include(c => c.Chemical)
                .Include(c => c.Customer);
            if (!string.IsNullOrEmpty(saleOrderResource.SalesOrderNumber))
            {
                // trim & ignore casing
                var genreForWhereClause = saleOrderResource.SalesOrderNumber
                    .Trim().ToLowerInvariant();

                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.SalesOrderNumber, $"{genreForWhereClause}%"));
            }

            if (saleOrderResource.SalesOrderDate != null)
            {

                var minDate = new DateTime(saleOrderResource.SalesOrderDate.Value.Year, saleOrderResource.SalesOrderDate.Value.Month, saleOrderResource.SalesOrderDate.Value.Day, 0, 0, 0);

                var maxDate = new DateTime(saleOrderResource.SalesOrderDate.Value.Year, saleOrderResource.SalesOrderDate.Value.Month, saleOrderResource.SalesOrderDate.Value.Day, 23, 59, 59);

                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.SalesOrderDate >= minDate && c.SalesOrderDate <= maxDate);
            }
            if (saleOrderResource.ChemicalId != null)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.ChemicalId == saleOrderResource.ChemicalId);
            }
            if (saleOrderResource.CustomerId != null)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.CustomerId == saleOrderResource.CustomerId);
            }
            var salesOrderList = new SalesOrderList();
            return await  salesOrderList.Create(collectionBeforePaging, saleOrderResource.Skip, saleOrderResource.PageSize);
        }
    }
}