using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{

    public class GetCategoriesWithChemicalsQuery : IRequest<List<ChemicalCategoryDto>>
    {
      
    }
}
