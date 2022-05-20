using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class ChemicalCustomerRepository : GenericRepository<ChemicalCustomer, ChemWebsiteDbContext>, IChemicalCustomerRepository
    {
        public ChemicalCustomerRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
