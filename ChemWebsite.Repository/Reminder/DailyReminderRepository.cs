using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class DailyReminderRepository : GenericRepository<DailyReminder, ChemWebsiteDbContext>,
        IDailyReminderRepository
    {
        public DailyReminderRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
