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
    public class GetRelatedChemicalsQueryHandler : IRequestHandler<GetRelatedChemicalsQuery, List<RelatedChemicalDto>>
    {
        private readonly IChemicalCategoryRepository _chemicalCategoryRepository;

        private readonly IIndustryChemicalRepository _industryChemicalRepository;
        public GetRelatedChemicalsQueryHandler(IChemicalCategoryRepository chemicalCategoryRepository,
            IIndustryChemicalRepository industryChemicalRepository)
        {
            _chemicalCategoryRepository = chemicalCategoryRepository;
            _industryChemicalRepository = industryChemicalRepository;
        }
        public async Task<List<RelatedChemicalDto>> Handle(GetRelatedChemicalsQuery request, CancellationToken cancellationToken)
        {
            var categoryIds = await _chemicalCategoryRepository.All
                .Where(c => c.ChemicalId == request.Id)
                .Select(c => c.CategoryId)
                .ToListAsync();
            var industryIds = await _industryChemicalRepository.All
                .Where(c => c.ChemicalId == request.Id)
                .Select(c => c.IndustryId)
                .ToListAsync();
            var chemicals = new List<RelatedChemicalDto>();

            foreach (var categoryId in categoryIds.Distinct())
            {
                var sameCategoryChemicals = _chemicalCategoryRepository.All
                .Where(c => categoryId == c.CategoryId)
                .Take(10)
                .Select(c => new RelatedChemicalDto
                {
                    CasNumber = c.Chemical.CasNumber,
                    Name = c.Chemical.Name,
                    MolecularFormulla = c.Chemical.MolecularFormulla,
                    CategoryName = c.Category.Name
                }).ToList();
                chemicals.AddRange(sameCategoryChemicals);
            }

            foreach (var industryId in industryIds.Distinct())
            {
                var sameIndustryChemicals = _industryChemicalRepository.All
               .Where(c => industryId == c.IndustryId)
               .Take(10)
               .Select(cs => new RelatedChemicalDto
               {
                   CasNumber = cs.Chemical.CasNumber,
                   Name = cs.Chemical.Name,
                   MolecularFormulla = cs.Chemical.MolecularFormulla,
                   IndustryName = cs.Industry.Name
               }).ToList();
                chemicals.AddRange(sameIndustryChemicals);
            }

            return chemicals;
        }
    }
}
