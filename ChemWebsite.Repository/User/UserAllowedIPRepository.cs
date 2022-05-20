using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class UserAllowedIPRepository : GenericRepository<UserAllowedIP, ChemWebsiteDbContext>,
        IUserAllowedIPRepository
    {
        public UserAllowedIPRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
