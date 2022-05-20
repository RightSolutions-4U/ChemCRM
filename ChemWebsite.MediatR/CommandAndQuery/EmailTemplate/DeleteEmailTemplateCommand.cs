using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteEmailTemplateCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
