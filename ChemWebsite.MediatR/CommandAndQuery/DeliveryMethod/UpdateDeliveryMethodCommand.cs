
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateDeliveryMethodCommand : IRequest<ServiceResponse<bool>>
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
