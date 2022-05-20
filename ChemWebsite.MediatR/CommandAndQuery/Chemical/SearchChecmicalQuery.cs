using ChemWebsite.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class SearchChecmicalQuery : IRequest<List<ChemicalDto>>
    {
        public string SearchQuery { get; set; }
        public string SearchBy { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
