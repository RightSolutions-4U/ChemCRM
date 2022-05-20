using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetAllArticleCategoriesQuery : IRequest<List<ArticleCategoryDto>>
    {
    }
}
