using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class ChemicalCategoryRepository : GenericRepository<ChemicalCategory, ChemWebsiteDbContext>, IChemicalCategoryRepository
    {
        public ChemicalCategoryRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
