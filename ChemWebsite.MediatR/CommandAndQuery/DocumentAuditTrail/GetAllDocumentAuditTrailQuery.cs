using ChemWebsite.Data.Dto;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.Queries
{
    public class GetAllDocumentAuditTrailQuery : IRequest<DocumentAuditTrailList>
    {
        public DocumentResource DocumentResource { get; set; }
    }
}
