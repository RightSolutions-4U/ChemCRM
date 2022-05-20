using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;
using ChemWebsite.Helper;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IContactUsRepository : IGenericRepository<ContactRequest>
    {
        Task<PagedList<ContactRequest>> GetContactUsList(ContactUsResource contactUsResource);
    }
}
