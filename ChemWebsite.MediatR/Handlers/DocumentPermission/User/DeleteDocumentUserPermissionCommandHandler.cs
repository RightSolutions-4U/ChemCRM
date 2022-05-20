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
    public class DeleteDocumentUserPermissionCommandHandler
        : IRequestHandler<DeleteDocumentUserPermissionCommand, ServiceResponse<DocumentUserPermissionDto>>
    {
        private readonly IDocumentUserPermissionRepository _documentUserPermissionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly UserInfoToken _userInfo;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        public DeleteDocumentUserPermissionCommandHandler(
           IDocumentUserPermissionRepository documentUserPermissionRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
              UserInfoToken userInfo,
              IDocumentAuditTrailRepository documentAuditTrailRepository
            )
        {
            _documentUserPermissionRepository = documentUserPermissionRepository;
            _uow = uow;
            _userInfo = userInfo;
            _documentAuditTrailRepository = documentAuditTrailRepository;
        }

        public async Task<ServiceResponse<DocumentUserPermissionDto>> Handle(DeleteDocumentUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _documentUserPermissionRepository.FindAsync(request.Id);
            if (entity == null)
            {
                return ServiceResponse<DocumentUserPermissionDto>.Return404("Not Found");
            }

            var documentAudit = new DocumentAuditTrail()
            {
                DocumentId = entity.DocumentId,
                CreatedBy = Guid.Parse(_userInfo.Id),
                CreatedDate = DateTime.Now,
                OperationName = DocumentOperation.Remove_Permission,
                AssignToUserId = entity.UserId
            };
            _documentAuditTrailRepository.Add(documentAudit);
            _documentUserPermissionRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentUserPermissionDto>.Return500();
            }
            return ServiceResponse<DocumentUserPermissionDto>.ReturnResultWith200(null);
        }
    }
}
