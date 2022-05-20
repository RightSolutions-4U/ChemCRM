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
    public class GetCategoriesWithChemicalsQueryHandler : IRequestHandler<GetCategoriesWithChemicalsQuery, List<ChemicalCategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesWithChemicalsQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<ChemicalCategoryDto>> Handle(GetCategoriesWithChemicalsQuery request, CancellationToken cancellationToken)
        {
            var categoryWithChemicals = await _categoryRepository.All.Where(cc => cc.ChemicalCategories.Any())
                  .Select(c => new ChemicalCategoryDto
                  {
                      Category = new CategoryDto
                      {
                          Id = c.Id,
                          Name = c.Name,
                          Description = c.Description
                      },
                      Chemicals = c.ChemicalCategories.Select(cd => new ChemicalDto
                      {
                          Id = cd.Chemical.Id,
                          Name = cd.Chemical.Name,
                          CasNumber = cd.Chemical.CasNumber,
                          MolecularFormulla = cd.Chemical.MolecularFormulla
                      }).OrderBy(c => c.CasNumber).Take(10).ToList(),
                      TotalChemicals = c.ChemicalCategories.Count
                  }).ToListAsync();
            return categoryWithChemicals;
        }
    }
}
