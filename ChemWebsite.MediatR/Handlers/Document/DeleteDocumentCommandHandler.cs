using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public DeleteDocumentCommandHandler(
           IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DocumentDto>> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _documentRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(404, "Document already exist.");
            }

            _documentRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentDto>(entityExist);
            return ServiceResponse<DocumentDto>.ReturnResultWith201(entityDto);
        }
    }
}
