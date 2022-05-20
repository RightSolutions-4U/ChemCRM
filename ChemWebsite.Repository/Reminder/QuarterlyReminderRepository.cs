using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class QuarterlyReminderRepository : GenericRepository<QuarterlyReminder, ChemWebsiteDbContext>,
    IQuarterlyReminderRepository
    {
        public QuarterlyReminderRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
