using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteDeliveryMethodCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}

