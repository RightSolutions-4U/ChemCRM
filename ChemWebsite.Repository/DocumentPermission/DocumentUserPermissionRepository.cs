
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class DocumentUserPermissionRepository : GenericRepository<DocumentUserPermission, ChemWebsiteDbContext>,
       IDocumentUserPermissionRepository
    {
        public DocumentUserPermissionRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
