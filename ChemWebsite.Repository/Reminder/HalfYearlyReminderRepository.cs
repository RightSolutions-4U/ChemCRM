using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class HalfYearlyReminderRepository : GenericRepository<HalfYearlyReminder, ChemWebsiteDbContext>,
        IHalfYearlyReminderRepository
    {
        public HalfYearlyReminderRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
