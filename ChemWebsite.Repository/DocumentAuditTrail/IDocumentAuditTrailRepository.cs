using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public interface IDocumentAuditTrailRepository : IGenericRepository<DocumentAuditTrail>
    {
        Task<DocumentAuditTrailList> GetDocumentAuditTrails(DocumentResource documentResource);
    }
}
