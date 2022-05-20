using AutoMapper;
using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.Queries;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Commands
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, DocumentDto>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IMapper _mapper;
        public GetDocumentByIdQueryHandler(
           IDocumentRepository documentRepository,
             IMapper mapper
            )
        {
            _mapper = mapper;
            _documentRepository = documentRepository;
        }

        public async Task<DocumentDto> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _documentRepository.FindAsync(request.Id);
            var documentDto = _mapper.Map<DocumentDto>(document);
            return documentDto;
        }
    }
}
