using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class ChemicalSynonymRepository : GenericRepository<ChemicalSynonym, ChemWebsiteDbContext>, IChemicalSynonymRepository
    {
        public ChemicalSynonymRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
