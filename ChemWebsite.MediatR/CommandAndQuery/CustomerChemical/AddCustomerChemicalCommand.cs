using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class AddCustomerChemicalCommand : IRequest<ServiceResponse<List<ChemicalDto>>>
    {
        public List<Guid> ChemicalIdList { get; set; }
        public Guid CustomerId { get; set; }
    }
}
