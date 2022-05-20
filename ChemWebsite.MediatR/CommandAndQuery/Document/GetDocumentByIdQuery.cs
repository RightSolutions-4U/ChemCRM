using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Queries
{
    public class GetDocumentByIdQuery : IRequest<DocumentDto>
    {
        public Guid Id { get; set; }
    }
}
