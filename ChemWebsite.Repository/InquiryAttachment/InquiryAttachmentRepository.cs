using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class InquiryAttachmentRepository : GenericRepository<InquiryAttachment, ChemWebsiteDbContext>, IInquiryAttachmentRepository
    {
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public InquiryAttachmentRepository(IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {

        }
    }
}
