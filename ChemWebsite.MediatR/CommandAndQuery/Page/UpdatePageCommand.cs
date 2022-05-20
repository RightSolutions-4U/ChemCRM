using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class UpdatePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
