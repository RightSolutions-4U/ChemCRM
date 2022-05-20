using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class PurchaseOrderRepository
        : GenericRepository<PurchaseOrder, ChemWebsiteDbContext>, IPurchaseOrderRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;

        public PurchaseOrderRepository(IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<PurchaseOrderList> GetAllPurchaseOrders(PurchaseOrderResource purchaseOrderResource)
        {
            var collectionBeforePaging = AllIncluding(c => c.Supplier, cs => cs.Chemical).ApplySort(purchaseOrderResource.OrderBy,
                _propertyMappingService.GetPropertyMapping<PurchaseOrderDto, PurchaseOrder>());

            if (purchaseOrderResource.SupplierId.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.SupplierId == purchaseOrderResource.SupplierId);
            }

            if (!string.IsNullOrWhiteSpace(purchaseOrderResource.ChemicalName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Chemical.Name == purchaseOrderResource.ChemicalName);
            }

            if (!string.IsNullOrWhiteSpace(purchaseOrderResource.SupplierName))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.Supplier.SupplierName == purchaseOrderResource.SupplierName.GetUnescapestring());
            }

            if (purchaseOrderResource.POCreatedDate.HasValue)
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => a.POCreatedDate == purchaseOrderResource.POCreatedDate);
            }

            if (!string.IsNullOrWhiteSpace(purchaseOrderResource.OrderNumber))
            {
                var orderNumber = purchaseOrderResource.OrderNumber.Trim();
                collectionBeforePaging = collectionBeforePaging
                    .Where(a => EF.Functions.Like(a.OrderNumber, $"%{orderNumber}%"));
            }

            var purchaseOrders = new PurchaseOrderList();
            return await purchaseOrders
                .Create(collectionBeforePaging, purchaseOrderResource.Skip, purchaseOrderResource.PageSize);
        }
    }
}
