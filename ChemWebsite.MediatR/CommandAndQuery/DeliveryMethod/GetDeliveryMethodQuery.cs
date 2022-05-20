using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetDeliveryMethodQuery : IRequest<ServiceResponse<DeliveryMethodDto>>
    {
        public Guid Id { get; set; }
    }
}
