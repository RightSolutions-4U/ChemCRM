using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class ReminderFrequencyRepository
        : GenericRepository<ReminderFrequency, ChemWebsiteDbContext>, IReminderFrequencyRepository
    {
        public ReminderFrequencyRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
