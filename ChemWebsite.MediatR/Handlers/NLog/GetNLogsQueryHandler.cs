using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetNLogsQueryHandler : IRequestHandler<GetNLogsQuery, NLogList>
    {
        private readonly INLogRepository _nLogRepository;
        public GetNLogsQueryHandler(INLogRepository nLogRepository)
        {
            _nLogRepository = nLogRepository;
        }
        public async Task<NLogList> Handle(GetNLogsQuery request, CancellationToken cancellationToken)
        {
            return await _nLogRepository.GetNLogsAsync(request.NLogResource);
        }
    }
}
