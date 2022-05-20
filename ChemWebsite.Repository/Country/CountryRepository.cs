using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class CountryRepository : GenericRepository<Country, ChemWebsiteDbContext>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
          : base(uow)
        {
        }
    }
}
