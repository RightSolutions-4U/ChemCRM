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
    public class DocumentPermissionUserRoleCommandHandler : IRequestHandler<DocumentPermissionUserRoleCommand, ServiceResponse<bool>>
    {
        private readonly IDocumentRolePermissionRepository _documentRolePermissionRepository;
        private readonly IDocumentUserPermissionRepository _documentUserPermissionRepository;
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly IDocumentAuditTrailRepository _documentAuditTrailRepository;
        private readonly UserInfoToken _userInfo;


        public DocumentPermissionUserRoleCommandHandler(
            IDocumentRolePermissionRepository documentRolePermissionRepository,
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IMapper mapper,
            IDocumentAuditTrailRepository documentAuditTrailRepository,
            UserInfoToken userInfo,
            IDocumentUserPermissionRepository documentUserPermissionRepository)
        {
            _documentRolePermissionRepository = documentRolePermissionRepository;
            _uow = uow;
            _mapper = mapper;
            _documentAuditTrailRepository = documentAuditTrailRepository;
            _documentUserPermissionRepository = documentUserPermissionRepository;
            _userInfo = userInfo;
        }

        public async Task<ServiceResponse<bool>> Handle(DocumentPermissionUserRoleCommand request, CancellationToken cancellationToken)
        {
            if (request.IsTimeBound)
            {
                request.StartDate = new DateTime(request.StartDate.Value.Year, request.StartDate.Value.Month, request.StartDate.Value.Day).AddSeconds(1);
                request.EndDate = new DateTime(request.EndDate.Value.Year, request.EndDate.Value.Month, request.EndDate.Value.Day).AddDays(1).AddSeconds(-1);
            }
            List<DocumentAuditTrail> lstDocumentAuditTrail = new List<DocumentAuditTrail>();
            if (request.Roles != null && request.Roles.Count() > 0)
            {
                List<DocumentRolePermission> lstDocumentRolePermission = new List<DocumentRolePermission>();

                foreach (var document in request.Documents)
                {
                    foreach (var role in request.Roles)
                    {

                        lstDocumentRolePermission.Add(new DocumentRolePermission
                        {
                            DocumentId = Guid.Parse(document),
                            RoleId = Guid.Parse(role),
                            StartDate = request.StartDate,
                            EndDate = request.EndDate,
                            IsTimeBound = request.IsTimeBound,
                            IsAllowDownload = request.IsAllowDownload,
                            CreatedBy = Guid.Parse(_userInfo.Id),
                            CreatedDate = DateTime.Now
                        });

                        lstDocumentAuditTrail.Add(new DocumentAuditTrail()
                        {
                            DocumentId = Guid.Parse(document),
                            CreatedBy = Guid.Parse(_userInfo.Id),
                            CreatedDate = DateTime.Now,
                            OperationName = DocumentOperation.Add_Permission,
                            AssignToRoleId = Guid.Parse(role)
                        });
                    }
                    List<Guid> roles = request.Roles.Select(c => Guid.Parse(c)).ToList();
                }
                _documentRolePermissionRepository.AddRange(lstDocumentRolePermission);
            }

            if (request.Users != null && request.Users.Count() > 0)
            {
                List<DocumentUserPermission> lstDocumentUserPermission = new List<DocumentUserPermission>();

                foreach (var document in request.Documents)
                {
                    foreach (var user in request.Users)
                    {

                        lstDocumentUserPermission.Add(new DocumentUserPermission
                        {
                            DocumentId = Guid.Parse(document),
                            UserId = Guid.Parse(user),
                            StartDate = request.StartDate,
                            EndDate = request.EndDate,
                            IsTimeBound = request.IsTimeBound,
                            IsAllowDownload = request.IsAllowDownload,
                            CreatedBy = Guid.Parse(_userInfo.Id),
                            CreatedDate = DateTime.Now
                        });

                        lstDocumentAuditTrail.Add(new DocumentAuditTrail()
                        {
                            DocumentId = Guid.Parse(document),
                            CreatedBy = Guid.Parse(_userInfo.Id),
                            CreatedDate = DateTime.Now,
                            OperationName = DocumentOperation.Add_Permission,
                            AssignToUserId = Guid.Parse(user)
                        });

                    }
                    List<Guid> users = request.Users.Select(c => Guid.Parse(c)).ToList();
                }
                _documentUserPermissionRepository.AddRange(lstDocumentUserPermission);
            }

            if (lstDocumentAuditTrail.Count() > 0)
            {
                _documentAuditTrailRepository.AddRange(lstDocumentAuditTrail);
            }

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
