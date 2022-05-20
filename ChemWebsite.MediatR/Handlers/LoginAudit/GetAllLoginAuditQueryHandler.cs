using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllLoginAuditQueryHandler : IRequestHandler<GetAllLoginAuditQuery, LoginAuditList>
    {
        private readonly ILoginAuditRepository _loginAuditRepository;
        public GetAllLoginAuditQueryHandler(ILoginAuditRepository loginAuditRepository)
        {
            _loginAuditRepository = loginAuditRepository;
        }
        public async Task<LoginAuditList> Handle(GetAllLoginAuditQuery request, CancellationToken cancellationToken)
        {
            return await _loginAuditRepository.GetDocumentAuditTrails(request.LoginAuditResource);
        }
    }
}
