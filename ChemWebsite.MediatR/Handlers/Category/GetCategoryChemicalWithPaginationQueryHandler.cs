using ChemWebsite.Data.Dto;
using ChemWebsite.MediatR.CommandAndQuery;
using ChemWebsite.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.Handler.Category
{
    public class GetCategoryChemicalWithPaginationQueryHandler : IRequestHandler<GetCategoryChemicalWithPaginationQuery, List<ChemicalDto>>
    {
        private readonly IChemicalCategoryRepository _chemicalCategoryRepository;
        public GetCategoryChemicalWithPaginationQueryHandler(IChemicalCategoryRepository chemicalCategoryRepository)
        {
            _chemicalCategoryRepository = chemicalCategoryRepository;
        }
        public async Task<List<ChemicalDto>> Handle(GetCategoryChemicalWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var recordsToSkip = request.PageSize * (request.PageNumber - 1);
            var chemicals = await _chemicalCategoryRepository.All.Where(cc => cc.CategoryId == request.CategoryId)
                .Select(cd => new ChemicalDto
                {
                    Id = cd.Chemical.Id,
                    Name = cd.Chemical.Name,
                    CasNumber = cd.Chemical.CasNumber,
                    MolecularFormulla = cd.Chemical.MolecularFormulla
                }).OrderBy(c => c.CasNumber)
                .Skip(recordsToSkip)
                .Take(request.PageSize)
                .ToListAsync();
            return chemicals;
        }
    }
}
