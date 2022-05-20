using ChemWebsite.Data;
using ChemWebsite.Helper;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetAllContactUsQueryHandler : IRequestHandler<GetAllContactUsQuery, PagedList<ContactRequest>>
    {
        private readonly IContactUsRepository _contactUsRepository;
        public GetAllContactUsQueryHandler(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        public async Task<PagedList<ContactRequest>> Handle(GetAllContactUsQuery request, CancellationToken cancellationToken)
        {
            return await _contactUsRepository.GetContactUsList(request.ContactUsResource);
        }
    }
}
