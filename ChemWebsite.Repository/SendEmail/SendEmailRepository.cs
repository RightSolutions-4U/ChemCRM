using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
   public class SendEmailRepository : GenericRepository<SendEmail, ChemWebsiteDbContext>,
          ISendEmailRepository
    {
        public SendEmailRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
