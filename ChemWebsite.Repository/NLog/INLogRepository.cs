using System.Threading.Tasks;
using ChemWebsite.Common.GenericRepository;
using ChemWebsite.Data;
using ChemWebsite.Data.Resources;

namespace ChemWebsite.Repository
{
    public interface INLogRepository : IGenericRepository<NLog>
    {
        Task<NLogList> GetNLogsAsync(NLogResource nLogResource);
    }
}
