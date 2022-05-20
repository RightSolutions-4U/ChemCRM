using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteIndustryCommand : IRequest<ServiceResponse<IndustryDto>>
    {
        public Guid Id { get; set; }
    }
}
