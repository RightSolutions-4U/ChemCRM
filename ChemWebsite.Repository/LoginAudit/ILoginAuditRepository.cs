using System.Threading.Tasks;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;

namespace ChemWebsite.Repository
{
    public interface ILoginAuditRepository : IGenericRepository<LoginAudit>
    {
        Task<LoginAuditList> GetDocumentAuditTrails(LoginAuditResource loginAuditResrouce);
        Task LoginAudit(LoginAuditDto loginAudit);
    }
}
