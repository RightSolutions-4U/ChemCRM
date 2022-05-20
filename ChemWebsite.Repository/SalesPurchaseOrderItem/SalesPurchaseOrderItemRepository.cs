using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
   public class SalesPurchaseOrderItemRepository : GenericRepository<SalesPurchaseOrderItem, ChemWebsiteDbContext>,
          ISalesPurchaseOrderItemRepository
    {
        public SalesPurchaseOrderItemRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
