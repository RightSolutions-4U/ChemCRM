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
    public class DeleteIndustryChemicalCommand : IRequest<ServiceResponse<IndustryDto>>
    {
        public Guid IndustryId { get; set; }
        public Guid ChemicalId { get; set; }
    }
}
