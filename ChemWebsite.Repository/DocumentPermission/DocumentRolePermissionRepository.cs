
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class DocumentRolePermissionRepository : GenericRepository<DocumentRolePermission, ChemWebsiteDbContext>,
        IDocumentRolePermissionRepository
    {
        private readonly IUnitOfWork<ChemWebsiteDbContext> _uow;
        public DocumentRolePermissionRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
            _uow = uow;
        }
    }
}
