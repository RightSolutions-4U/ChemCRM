using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteChemicalCommand : IRequest<ServiceResponse<ChemicalDto>>
    {
        public Guid Id { get; set; }
    }
}
