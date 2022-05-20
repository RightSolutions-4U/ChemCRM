using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class SearchSupplierQuery : IRequest<List<SupplierDto>>
    {
        public string SearchQuery { get; set; }
        public int PageSize { get; set; } = 10;
    }
}
