using MediatR;
using ChemWebsite.Data.Resources;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllLoginAuditQuery : IRequest<LoginAuditList>
    {
        public LoginAuditResource LoginAuditResource { get; set; }
    }
}
