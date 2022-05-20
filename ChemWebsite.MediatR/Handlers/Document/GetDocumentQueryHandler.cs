using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Queries;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetDocumentQueryHandler : IRequestHandler<GetDocumentQuery, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        public GetDocumentQueryHandler(
           IDocumentRepository documentRepository,
             IMapper mapper
            )
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
        }
        public async Task<ServiceResponse<DocumentDto>> Handle(GetDocumentQuery request, CancellationToken cancellationToken)
        {
            var entity = await _documentRepository.GetDocumentById(request.Id);
            // mark notification as read.
            if (entity != null)
            {
                return ServiceResponse<DocumentDto>.ReturnResultWith200(_mapper.Map<DocumentDto>(entity));
            }
            else
                return ServiceResponse<DocumentDto>.ReturnFailed(404, "Document is not found.");
        }
    }
}
