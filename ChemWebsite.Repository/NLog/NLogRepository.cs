using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Common.UnitOfWork;
using ChemWebsite.Data;
using ChemWebsite.Data.Dto;
using ChemWebsite.Data.Resources;
using ChemWebsite.Domain;

namespace ChemWebsite.Repository
{
    public class NLogRepository : GenericRepository<NLog, ChemWebsiteDbContext>,
          INLogRepository
    {
        private readonly IPropertyMappingService _propertyMappingService;
        public NLogRepository(IUnitOfWork<ChemWebsiteDbContext> uow,
            IPropertyMappingService propertyMappingService) : base(uow)
        {
            _propertyMappingService = propertyMappingService;
        }

        public async Task<NLogList> GetNLogsAsync(NLogResource nLogResource)
        {
            var collectionBeforePaging = All;
            collectionBeforePaging =
               collectionBeforePaging.ApplySort(nLogResource.OrderBy,
               _propertyMappingService.GetPropertyMapping<NLogDto, NLog>());

            if (!string.IsNullOrWhiteSpace(nLogResource.Message))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => EF.Functions.Like(c.Message, $"%{nLogResource.Message.Trim()}%"));
            }

            if (!string.IsNullOrWhiteSpace(nLogResource.Level))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.Level == nLogResource.Level);
            }

            if (!string.IsNullOrWhiteSpace(nLogResource.Source))
            {
                collectionBeforePaging = collectionBeforePaging
                    .Where(c => c.Source == nLogResource.Source);
            }

            var nLogList = new NLogList();
            return await nLogList.Create(
                collectionBeforePaging,
                nLogResource.Skip,
                nLogResource.PageSize
                );
        }
    }
}
