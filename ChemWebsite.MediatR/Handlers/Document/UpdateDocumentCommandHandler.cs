using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, ServiceResponse<DocumentDto>>
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        public UpdateDocumentCommandHandler(
           IDocumentRepository documentRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow
            )
        {
            _documentRepository = documentRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<DocumentDto>> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _documentRepository.FindBy(c => c.Name == request.Name && c.Id!= request.Id).FirstOrDefaultAsync();
            if (entityExist != null)
            {
                return ServiceResponse<DocumentDto>.ReturnFailed(404, "Document is not found.");
            }
            var entity = _mapper.Map<Document>(request);
            entityExist = await _documentRepository.FindBy(v => v.Id == request.Id).FirstOrDefaultAsync();
            entity.CreatedBy = entityExist.CreatedBy;
            entity.CreatedDate = entityExist.CreatedDate;
            entity.Url = entityExist.Url;
            _documentRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentDto>(entity);

            return ServiceResponse<DocumentDto>.ReturnResultWith200(entityDto);
        }
    }
}
