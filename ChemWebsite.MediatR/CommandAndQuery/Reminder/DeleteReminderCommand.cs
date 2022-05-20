using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteReminderCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
