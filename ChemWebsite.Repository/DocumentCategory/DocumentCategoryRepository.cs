
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data.Entities;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class DocumentCategoryRepository : GenericRepository<DocumentCategory, ChemWebsiteDbContext>,
           IDocumentCategoryRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public DocumentCategoryRepository(
            IUnitOfWork<ChemWebsiteDbContext> uow
            ) : base(uow)
        {
        }
    }
}
