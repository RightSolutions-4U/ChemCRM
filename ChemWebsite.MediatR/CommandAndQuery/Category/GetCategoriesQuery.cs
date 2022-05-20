using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCategoriesQuery : IRequest<List<CategoryDto>>
    {
        public int PageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;

    }
}
