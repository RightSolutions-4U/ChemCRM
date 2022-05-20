using AutoMapper;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Dto.Document;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.Commands;
using ChemWebsite.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddDocumentAuditTrailCommandHandler : IRequestHandler<AddDocumentAuditTrailCommand, ServiceResponse<DocumentAuditTrailDto>>
    {
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly UserInfoToken _userInfo;
        public AddDocumentAuditTrailCommandHandler(
           IDocumentAuditTrailRepository documentAuditTrailRepository,
            IMapper mapper,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfo
            )
        {
            _documentAuditTrailRepository = documentAuditTrailRepository;
            _mapper = mapper;
            _uow = uow;
            _userInfo = userInfo;
        }
        public async Task<ServiceResponse<DocumentAuditTrailDto>> Handle(AddDocumentAuditTrailCommand request, CancellationToken cancellationToken)
        {
            var entity = new DocumentAuditTrail();
            entity.DocumentId = request.DocumentId;
            entity.CreatedBy = Guid.Parse(_userInfo.Id);
            entity.CreatedDate = new DateTime();
            entity.OperationName = ParseEnum(request.OperationName);
            _documentAuditTrailRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentAuditTrailDto>.Return500();
            }
            var entityDto = _mapper.Map<DocumentAuditTrailDto>(entity);
            return ServiceResponse<DocumentAuditTrailDto>.ReturnResultWith200(entityDto);
        }
        public DocumentOperation ParseEnum(string value)
        {
            return (DocumentOperation)Enum.Parse(typeof(DocumentOperation), value, true);
        }
    }
}
