using ChemWebsite.Helper;
using MediatR;
using System;


namespace ChemWebsite.MediatR.CommandAndQuery
{
   public class UpdatePaymentTermCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}

