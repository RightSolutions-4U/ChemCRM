using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class InquiryActivityRepository : GenericRepository<InquiryActivity, ChemWebsiteDbContext>, IInquiryActivityRepository
    {
        public InquiryActivityRepository(IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {

        }
    }
}
