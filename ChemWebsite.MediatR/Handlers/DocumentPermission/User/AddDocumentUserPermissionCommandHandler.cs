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
    public class AddDocumentUserPermissionCommandHandler
        : IRequestHandler<AddDocumentUserPermissionCommand, ServiceResponse<DocumentUserPermissionDto>>
    {
        IDocumentUserPermissionRepository _documentUserPermissionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        private readonly UserInfoToken _userInfo;

        public AddDocumentUserPermissionCommandHandler(
            IDocumentUserPermissionRepository documentUserPermissionRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
               IDocumentAuditTrailRepository documentAuditTrailRepository,
            UserInfoToken userInfo)
        {
            _documentUserPermissionRepository = documentUserPermissionRepository;
            _uow = uow;
            _mapper = mapper;
            _documentAuditTrailRepository = documentAuditTrailRepository;
            _userInfo = userInfo;
        }
        public async Task<ServiceResponse<DocumentUserPermissionDto>> Handle(AddDocumentUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var permissions = _mapper.Map<List<DocumentUserPermission>>(request.DocumentUserPermissions);
            permissions.ForEach(permission =>
            {
                if (permission.IsTimeBound)
                {
                    permission.StartDate = new DateTime(permission.StartDate.Value.Year, permission.StartDate.Value.Month, permission.StartDate.Value.Day).AddSeconds(1);
                    permission.EndDate = new DateTime(permission.EndDate.Value.Year, permission.EndDate.Value.Month, permission.EndDate.Value.Day).AddDays(1).AddSeconds(-1);
                }
            });
            _documentUserPermissionRepository.AddRange(permissions);
            var userIds = request.DocumentUserPermissions.Select(c => c.UserId).ToList();
            var documentId = request.DocumentUserPermissions.FirstOrDefault().DocumentId;

            List<DocumentAuditTrail> lstDocumentAuditTrail = new List<DocumentAuditTrail>();
            foreach (var userId in userIds)
            {
                var documentAudit = new DocumentAuditTrail()
                {
                    DocumentId = documentId,
                    CreatedBy = Guid.Parse(_userInfo.Id),
                    CreatedDate = DateTime.Now,
                    OperationName = DocumentOperation.Add_Permission,
                    AssignToUserId = userId
                };
                lstDocumentAuditTrail.Add(documentAudit);
            }
            if (lstDocumentAuditTrail.Count() > 0)
            {
                _documentAuditTrailRepository.AddRange(lstDocumentAuditTrail);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<DocumentUserPermissionDto>.Return500();
            }

            return ServiceResponse<DocumentUserPermissionDto>.ReturnResultWith200(null);
        }
    }
}
