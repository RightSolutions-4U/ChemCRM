using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
   public class PaymentTermRepository : GenericRepository<PaymentTerm, ChemWebsiteDbContext>,
          IPaymentTermRepository
    {
        public PaymentTermRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}