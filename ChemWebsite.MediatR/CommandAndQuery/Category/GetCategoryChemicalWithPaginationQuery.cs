using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetCategoryChemicalWithPaginationQuery : IRequest<List<ChemicalDto>>
    {
        public Guid CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
