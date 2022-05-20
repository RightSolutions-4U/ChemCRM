using ChemWebsite.MediatR.Queries;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllDocumentAuditTrailQueryHandler : IRequestHandler<GetAllDocumentAuditTrailQuery, DocumentAuditTrailList>
    {
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        public GetAllDocumentAuditTrailQueryHandler(
           IDocumentAuditTrailRepository documentAuditTrailRepository
            )
        {
            _documentAuditTrailRepository = documentAuditTrailRepository;

        }
        public async Task<DocumentAuditTrailList> Handle(GetAllDocumentAuditTrailQuery request, CancellationToken cancellationToken)
        {
            return await _documentAuditTrailRepository.GetDocumentAuditTrails(request.DocumentResource);
        }
    }
}
