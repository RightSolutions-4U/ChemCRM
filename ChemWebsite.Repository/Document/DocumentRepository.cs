using AutoMapper;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChemWebsite.Repository
{
    public class DocumentRepository : GenericRepository<Document, ChemWebsiteDbContext>,
          IDocumentRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        IUserRepository _userRepository;
        private readonly UserInfoToken _userInfoToken;
        private readonly IMapper _mapper;
        private readonly IDocumentRolePermissionRepository _documentRolePermissionRepository;
        private readonly IDocumentUserPermissionRepository _documentUserPermissionRepository;
        public DocumentRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService,
            IUserRepository userRepository,
            UserInfoToken userInfoToken,
            IMapper mapper,
            IDocumentRolePermissionRepository documentRolePermissionRepository,
            IDocumentUserPermissionRepository documentUserPermissionRepository
            ) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
            _userRepository = userRepository;
            _userInfoToken = userInfoToken;
            _mapper = mapper;
            _documentRolePermissionRepository = documentRolePermissionRepository;
            _documentUserPermissionRepository = documentUserPermissionRepository;
        }

        public async Task<DocumentList> GetDocuments(DocumentResource documentResource)
        {
            var collectionBeforePaging = AllIncluding(c => c.CreatedByUser);
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(documentResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<DocumentDto, Document>());

            if (!string.IsNullOrWhiteSpace(documentResource.Name))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Name, $"%{documentResource.Name}%"));
            }

            if (!string.IsNullOrWhiteSpace(documentResource.CategoryId))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.CategoryId == Guid.Parse(documentResource.CategoryId));
            }

            if (!string.IsNullOrWhiteSpace(documentResource.CreateDateString))
            {
                documentResource.CreateDate = DateTime.ParseExact(documentResource.CreateDateString, "dd/MM/yyyy", null);
                var minDate = new DateTime(documentResource.CreateDate.Value.Year, documentResource.CreateDate.Value.Month, documentResource.CreateDate.Value.Day, 0, 0, 0);

                var maxDate = new DateTime(documentResource.CreateDate.Value.Year, documentResource.CreateDate.Value.Month, documentResource.CreateDate.Value.Day, 23, 59, 59);

                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.CreatedDate >= minDate && c.CreatedDate <= maxDate);
            }
            var documentList = new DocumentList();
            return await documentList.Create(
                collectionBeforePaging,
                documentResource.Skip,
                documentResource.PageSize
                );
        }

        public async Task<DocumentList> GetDocumentsLibrary(string email, DocumentResource documentResource)
        {
            var today = DateTime.Now;
            var user = await _userRepository.AllIncluding(c => c.UserRoles).FirstOrDefaultAsync(c => c.Id == Guid.Parse(_userInfoToken.Id));
            var userRoles = user.UserRoles.Select(c => c.RoleId).ToList();
            var collectionBeforePaging = AllIncluding(c => c.CreatedByUser, c => c.DocumentRolePermissions, c => c.DocumentUserPermissions)
                                        .Where(d => (d.DocumentUserPermissions.Any(c => c.UserId == user.Id && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today)))
                                                    || (d.DocumentRolePermissions.Any(c => userRoles.Contains(c.RoleId)
                                                    && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today))))));

            collectionBeforePaging = collectionBeforePaging
                .ApplySort(documentResource.OrderBy, _propertyMappingService.GetPropertyMapping<DocumentDto, Document>());

            if (!string.IsNullOrWhiteSpace(documentResource.Name))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Name, $"%{documentResource.Name}%"));
            }
            if (!string.IsNullOrWhiteSpace(documentResource.CategoryId))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.CategoryId == Guid.Parse(documentResource.CategoryId));
            }

            var documentList = new DocumentList();
            return await documentList.CreateDocumentLibrary(
                collectionBeforePaging,
                documentResource.Skip,
                documentResource.PageSize
                );
        }

        public async Task<DocumentDto> GetDocumentById(Guid Id)
        {
            var today = DateTime.Now;
            var user = await _userRepository.AllIncluding(c => c.UserRoles).FirstOrDefaultAsync(c => c.Id == Guid.Parse(_userInfoToken.Id));
            var userRoles = user.UserRoles.Select(c => c.RoleId).ToList();
            var collectionBeforePaging = All
                                        .Where(d => (d.DocumentUserPermissions.Any(c => c.DocumentId == Id && c.UserId == user.Id && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today)))
                                                    || (d.DocumentRolePermissions.Any(c => c.DocumentId == Id && userRoles.Contains(c.RoleId) && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today))))));

            var document = await collectionBeforePaging.FirstOrDefaultAsync();

            if (document == null)
            {
                return null;
            }

            var result = _mapper.Map<DocumentDto>(document);
            result.IsAllowDownload = _documentUserPermissionRepository.All.Any(c => c.DocumentId == Id && c.IsAllowDownload && c.UserId == user.Id && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today)));
            if (result.IsAllowDownload)
            {
                return result;
            }
            result.IsAllowDownload = _documentRolePermissionRepository.All.Any(c => c.DocumentId == Id && c.IsAllowDownload && userRoles.Contains(c.RoleId) && (!c.IsTimeBound || (c.IsTimeBound && c.StartDate < today && c.EndDate > today)));
            return result;
        }
    }
}
