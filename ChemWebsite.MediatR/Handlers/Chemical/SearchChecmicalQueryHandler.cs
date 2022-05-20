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
    public class SearchChecmicalQueryHandler : IRequestHandler<SearchChecmicalQuery, List<ChemicalDto>>
    {

        private readonly IChemicalRepository _chemicalRepository;
        public SearchChecmicalQueryHandler(IChemicalRepository chemicalRepository)
        {
            _chemicalRepository = chemicalRepository;
        }
        public async Task<List<ChemicalDto>> Handle(SearchChecmicalQuery request, CancellationToken cancellationToken)
        {
            var chemicals = _chemicalRepository.All;
            if (!string.IsNullOrWhiteSpace(request.SearchQuery))
            {
                request.SearchQuery = request.SearchQuery.Trim();
            }

            if (request.SearchBy.ToLower() == "all")
            {
                chemicals = chemicals.Where(c => EF.Functions.Like(c.CasNumber, $"{request.SearchQuery}%")
                                || EF.Functions.Like(c.Name, $"{ request.SearchQuery}%"));
            }
            else if (request.SearchQuery.ToLower() == "casnumber")
            {
                chemicals = chemicals.Where(c => EF.Functions.Like(c.CasNumber, $"{request.SearchQuery}%"));
            }
            else
            {
                chemicals = chemicals.Where(c => EF.Functions.Like(c.Name, $"{request.SearchQuery}%"));
            }

            return await chemicals
                .OrderBy(c => c.Name)
                .Select(c => new ChemicalDto
                {
                    CasNumber = c.CasNumber,
                    Id = c.Id,
                    Name = c.Name
                }).Take(request.PageSize)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        }
    }
}
