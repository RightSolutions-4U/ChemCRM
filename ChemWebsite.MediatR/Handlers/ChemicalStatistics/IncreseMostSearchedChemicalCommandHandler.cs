using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class IncreseMostSearchedChemicalCommandHandler : IRequestHandler<IncreseMostSearchedChemicalCommand, bool>
    {
        private readonly IChemicalStatisticsRepository _chemicalStatisticsRepository;

        public IncreseMostSearchedChemicalCommandHandler(IChemicalStatisticsRepository chemicalStatisticsRepository)
        {
            _chemicalStatisticsRepository = chemicalStatisticsRepository;
        }

        public async Task<bool> Handle(IncreseMostSearchedChemicalCommand request, CancellationToken cancellationToken)
        {
            await _chemicalStatisticsRepository.IncreseMostSearchedChemical(request.Id);
            return true;
        }
    }
}
