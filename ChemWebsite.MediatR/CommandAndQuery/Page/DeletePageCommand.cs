using ChemWebsite.Data.Dto;
using MediatR;
using System;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeletePageCommand : IRequest<ServiceResponse<PageDto>>
    {
        public Guid Id { get; set; }
    }
}
