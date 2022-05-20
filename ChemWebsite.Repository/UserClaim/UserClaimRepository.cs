using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class UserClaimRepository : GenericRepository<UserClaim, ChemWebsiteDbContext>,
           IUserClaimRepository
    {
        public UserClaimRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}