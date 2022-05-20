using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class DeleteDocumentCommand : IRequest<ServiceResponse<DocumentDto>>
    {
        public Guid Id { get; set; }
    }
}
