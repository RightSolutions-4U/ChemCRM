using MediatR;
using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetNLogsQuery : IRequest<NLogList>
    {
        public NLogResource NLogResource { get; set; }
    }
}
