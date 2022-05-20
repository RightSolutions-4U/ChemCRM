using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class RoleClaimRepository : GenericRepository<RoleClaim, ChemWebsiteDbContext>,
           IRoleClaimRepository
    {
        public RoleClaimRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}