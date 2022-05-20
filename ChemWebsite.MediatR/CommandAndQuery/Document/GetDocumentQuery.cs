using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Queries
{
    public class GetDocumentQuery : IRequest<ServiceResponse<DocumentDto>>
    {
        public Guid Id { get; set; }
    }
}
