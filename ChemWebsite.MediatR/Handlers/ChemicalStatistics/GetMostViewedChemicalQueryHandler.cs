using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handlers
{
    public class GetMostViewedChemicalQueryHandler : IRequestHandler<GetMostViewedChemicalQuery, List<ChemicalStatisticsDto>>
    {
        private readonly IChemicalStatisticsRepository _chemicalStatisticsRepository;

        public GetMostViewedChemicalQueryHandler(IChemicalStatisticsRepository chemicalStatisticsRepository)
        {
            _chemicalStatisticsRepository = chemicalStatisticsRepository;
        }
        public async Task<List<ChemicalStatisticsDto>> Handle(GetMostViewedChemicalQuery request, CancellationToken cancellationToken)
        {
            return await _chemicalStatisticsRepository.AllIncluding(c => c.Chemical)
                .OrderByDescending(c => c.TotalView).Take(10).Select(c => new ChemicalStatisticsDto
                {
                    ChemicalId = c.ChemicalId,
                    CasNumber = c.Chemical.CasNumber,
                    ChemicalName = c.Chemical.Name,
                    Url = c.Chemical.Url
                }).ToListAsync();
        }
    }
}
