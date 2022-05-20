using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddIndustryChemicalCommand : IRequest<ServiceResponse<IndustryDto>>
    {
        public List<Guid> ChemicalIdList { get; set; }
        public Guid IndustryId { get; set; }
    }
}
