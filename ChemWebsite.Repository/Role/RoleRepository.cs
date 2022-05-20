using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public  class RoleRepository : GenericRepository<Role, ChemWebsiteDbContext>,
          IRoleRepository
    {
        public RoleRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}