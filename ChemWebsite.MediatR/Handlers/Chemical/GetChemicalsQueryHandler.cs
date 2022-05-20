using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetChemicalsQueryHandler : IRequestHandler<GetChemicalsQuery, ChemicalList>
    {

        private readonly IChemicalRepository _chemicalRepository;
        public GetChemicalsQueryHandler(IChemicalRepository chemicalRepository)
        {
            _chemicalRepository = chemicalRepository;
        }
        public async Task<ChemicalList> Handle(GetChemicalsQuery request, CancellationToken cancellationToken)
        {
            return await _chemicalRepository.GetChemicals(request.ChemicalResource);
        }
    }
}
