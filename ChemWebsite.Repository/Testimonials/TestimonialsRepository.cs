using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class TestimonialsRepository
        : GenericRepository<Testimonials, ChemWebsiteDbContext>, ITestimonialsRepository
    {
        public TestimonialsRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
          : base(uow)
        {
        }
    }
}
