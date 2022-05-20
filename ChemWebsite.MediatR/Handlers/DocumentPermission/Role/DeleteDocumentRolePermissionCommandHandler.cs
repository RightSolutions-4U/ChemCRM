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
    public class DeleteDocumentRolePermissionCommandHandler : IRequestHandler<DeleteDocumentRolePermissionCommand, ServiceResponse<DocumentRolePermissionDto>>
    {
        private readonly IDocumentRolePermissionRepository _documentRolePermissionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly UserInfoToken _userInfo;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        public DeleteDocumentRolePermissionCommandHandler(
           IDocumentRolePermissionRepository documentRolePermissionRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            UserInfoToken userInfo,
              IDocumentAuditTrailRepository documentAuditTrailRepository
            )
        {
            _documentRolePermissionRepository = documentRolePermissionRepository;
            _uow = uow;
            _userInfo = userInfo;
            _documentAuditTrailRepository = documentAuditTrailRepository;
        }

        public async Task<ServiceResponse<DocumentRolePermissionDto>> Handle(DeleteDocumentRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _documentRolePermissionRepository.FindAsync(request.Id);
            if (entity == null)
            {
                return ServiceResponse<DocumentRolePermissionDto>.Return404("Not Found.");
            }
            var documentAudit = new DocumentAuditTrail()
            {
                DocumentId = entity.DocumentId,
                CreatedBy = Guid.Parse(_userInfo.Id),
                CreatedDate = DateTime.Now,
                OperationName = DocumentOperation.Remove_Permission,
                AssignToRoleId = entity.RoleId
            };
            _documentAuditTrailRepository.Add(documentAudit);

            _documentRolePermissionRepository.Delete(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentRolePermissionDto>.Return500();
            }
            return ServiceResponse<DocumentRolePermissionDto>.ReturnResultWith200(null);
        }
    }
}
