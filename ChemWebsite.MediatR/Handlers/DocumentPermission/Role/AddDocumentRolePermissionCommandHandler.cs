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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class AddDocumentRolePermissionCommandHandler
         : IRequestHandler<AddDocumentRolePermissionCommand, ServiceResponse<DocumentRolePermissionDto>>
    {
        private readonly IDocumentRolePermissionRepository _documentRolePermissionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        private readonly UserInfoToken _userInfo;

        public AddDocumentRolePermissionCommandHandler(
            IDocumentRolePermissionRepository documentRolePermissionRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            IDocumentAuditTrailRepository documentAuditTrailRepository,
            UserInfoToken userInfo)
        {
            _documentRolePermissionRepository = documentRolePermissionRepository;
            _uow = uow;
            _mapper = mapper;
            _documentAuditTrailRepository = documentAuditTrailRepository;
            _userInfo = userInfo;
        }
        public async Task<ServiceResponse<DocumentRolePermissionDto>> Handle(AddDocumentRolePermissionCommand request, CancellationToken cancellationToken)
        {
            var permissions = _mapper.Map<List<DocumentRolePermission>>(request.DocumentRolePermissions);
            permissions.ForEach(permission =>
            {
                if (permission.IsTimeBound)
                {
                    permission.StartDate = new DateTime(permission.StartDate.Value.Year, permission.StartDate.Value.Month, permission.StartDate.Value.Day).AddSeconds(1);
                    permission.EndDate = new DateTime(permission.EndDate.Value.Year, permission.EndDate.Value.Month, permission.EndDate.Value.Day).AddDays(1).AddSeconds(-1);
                }
            });
            _documentRolePermissionRepository.AddRange(permissions);

            var documentId = request.DocumentRolePermissions.FirstOrDefault().DocumentId;
            var roleIds = request.DocumentRolePermissions.Select(c => c.RoleId).Distinct().ToList();
            List<DocumentAuditTrail> lstDocumentAuditTrail = new List<DocumentAuditTrail>();
            foreach (var roleId in roleIds)
            {
                var documentAudit = new DocumentAuditTrail()
                {
                    DocumentId = documentId,
                    CreatedBy = Guid.Parse(_userInfo.Id),
                    CreatedDate = DateTime.Now,
                    OperationName = DocumentOperation.Add_Permission,
                    AssignToRoleId = roleId
                };
                lstDocumentAuditTrail.Add(documentAudit);
            }
            if (lstDocumentAuditTrail.Count() > 0)
            {
                _documentAuditTrailRepository.AddRange(lstDocumentAuditTrail);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentRolePermissionDto>.Return500();
            }
            //var documentid = request.DocumentRolePermissions.FirstOrDefault().DocumentId;
            //var roleIds = request.DocumentRolePermissions.Select(c => c.RoleId).Distinct().ToList();

            return ServiceResponse<DocumentRolePermissionDto>.ReturnResultWith200(null);
        }
    }
}
