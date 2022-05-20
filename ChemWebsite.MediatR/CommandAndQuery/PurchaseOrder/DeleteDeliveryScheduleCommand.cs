using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteDeliveryScheduleCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
