using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;


namespace ChemWebsite.Repository
{
    public class DeliveryMethodRepository : GenericRepository<DeliveryMethod, ChemWebsiteDbContext>,
       IDeliveryMethodRepository
    {
        public DeliveryMethodRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {

        }
    }
}
