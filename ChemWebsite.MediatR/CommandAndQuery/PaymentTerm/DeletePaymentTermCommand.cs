using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeletePaymentTermCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
