using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetChemicalsByIndustryQuery : IRequest<ChemicalListDto>
    {
        public Guid Id { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public string ChemicalName { get; set; }
        public string CasNumber { get; set; }
    }
}
