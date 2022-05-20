using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class EmailSMTPSettingRepository : GenericRepository<EmailSMTPSetting, ChemWebsiteDbContext>,
           IEmailSMTPSettingRepository
    {
        public EmailSMTPSettingRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
