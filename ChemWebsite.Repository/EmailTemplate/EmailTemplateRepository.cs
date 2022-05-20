using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class EmailTemplateRepository : GenericRepository<EmailTemplate, ChemWebsiteDbContext>,
          IEmailTemplateRepository
    {
        public EmailTemplateRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {

        }
    }
}

