using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdateActionCommand : IRequest<ServiceResponse<ActionDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
