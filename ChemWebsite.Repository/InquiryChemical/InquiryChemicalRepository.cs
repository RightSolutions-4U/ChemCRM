using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class InquiryChemicalRepository : GenericRepository<InquiryChemical, ChemWebsiteDbContext>, IInquiryChemicalRepository
    {
        public InquiryChemicalRepository(IUnitOfWork<ChemWebsiteDbContext> uow) : base(uow)
        {
        }
    }
}
