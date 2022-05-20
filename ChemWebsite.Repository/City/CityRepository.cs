using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class CityRepository : GenericRepository<City, ChemWebsiteDbContext>, ICityRepository
    {
        public CityRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
          : base(uow)
        {
        }
    }
}