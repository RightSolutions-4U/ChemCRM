using ChemWebsite.Data.Dto;
using ChemWebsite.Repository;
using MediatR;

namespace ChemWebsite.MediatR.Queries
{
    public class GetAllDocumentQuery : IRequest<DocumentList>
    {
        public DocumentResource DocumentResource { get; set; }
    }
}
