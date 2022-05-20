using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class SupplierVerificationRepository : GenericRepository<SupplierVerification, ChemWebsiteDbContext>, ISupplierVerificationRepository
    {
        public SupplierVerificationRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
