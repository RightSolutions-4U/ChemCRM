using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class SupplierChemicalRepository : GenericRepository<ChemicalSupplier, ChemWebsiteDbContext>, ISupplierChemicalRepository
    {
        public SupplierChemicalRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}

