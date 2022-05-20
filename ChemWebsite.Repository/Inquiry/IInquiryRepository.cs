using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IInquiryRepository : IGenericRepository<Inquiry>
    {
        Task<InquiryList> GetInquiries(InquiryResource inquiryResource);
    }
}
