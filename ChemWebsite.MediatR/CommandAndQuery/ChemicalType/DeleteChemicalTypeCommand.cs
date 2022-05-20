using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.CommandAndQuery
{
    public class DeleteChemicalTypeCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
