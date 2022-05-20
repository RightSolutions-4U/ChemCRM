using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class ChemicalTypeRepository : GenericRepository<ChemicalType, ChemWebsiteDbContext>,
           IChemicalTypeRepository
    {
        public ChemicalTypeRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {

        }
    }
}
