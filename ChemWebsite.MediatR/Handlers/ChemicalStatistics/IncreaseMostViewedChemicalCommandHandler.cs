using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class IncreaseMostViewedChemicalCommandHandler : IRequestHandler<IncreaseMostViewedChemicalCommand, bool>
    {
        private readonly IChemicalStatisticsRepository _chemicalStatisticsRepository;

        public IncreaseMostViewedChemicalCommandHandler(IChemicalStatisticsRepository chemicalStatisticsRepository)
        {
            _chemicalStatisticsRepository = chemicalStatisticsRepository;
        }
        public async Task<bool> Handle(IncreaseMostViewedChemicalCommand request, CancellationToken cancellationToken)
        {
            await _chemicalStatisticsRepository.IncreseMostViewedChemical(request.Id);
            return true;
        }
    }
}
