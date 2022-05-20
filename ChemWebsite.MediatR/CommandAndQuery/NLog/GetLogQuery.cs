using MediatR;
using System;
using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class GetLogQuery : IRequest<ServiceResponse<NLogDto>>
    {
        public Guid Id { get; set; }
    }
}
