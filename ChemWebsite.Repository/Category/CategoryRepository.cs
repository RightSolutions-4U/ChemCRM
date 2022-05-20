using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class CategoryRepository : GenericRepository<Category, ChemWebsiteDbContext>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
