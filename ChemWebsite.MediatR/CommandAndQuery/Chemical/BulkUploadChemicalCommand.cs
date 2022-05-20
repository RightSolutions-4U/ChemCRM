using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class BulkUploadChemicalCommand : IRequest<ServiceResponse<bool>>
    {
        public List<ChemicalDto> Chemicals { get; set; }
    }
}
