using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class IndustryChemicalRepository : GenericRepository<ChemicalIndustry, ChemWebsiteDbContext>, IIndustryChemicalRepository
    {
        public IndustryChemicalRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
