using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteCustomerChemicalCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid CustomerId { get; set; }
        public Guid ChemicalId { get; set; }
    }
}
