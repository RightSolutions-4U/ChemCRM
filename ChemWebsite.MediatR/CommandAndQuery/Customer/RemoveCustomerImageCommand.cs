using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class RemoveCustomerImageCommand : IRequest<ServiceResponse<string>>
    {
        public Guid Id { get; set; }
    }
}
