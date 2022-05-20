using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Domain;
using ChemWebsite.Data;

namespace ChemWebsite.Repository
{
    public class NewsletterSubscriberRepository : GenericRepository<NewsletterSubscriber, ChemWebsiteDbContext>, INewsletterSubscriberRepository
    {
        public NewsletterSubscriberRepository(IUnitOfWork<ChemWebsiteDbContext> uow)
            : base(uow)
        {
        }
    }
}
