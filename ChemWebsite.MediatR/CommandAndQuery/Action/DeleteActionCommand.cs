using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteActionCommand : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
    }
}
