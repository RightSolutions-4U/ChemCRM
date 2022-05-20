using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class AddDocumentCommand : IRequest<ServiceResponse<DocumentDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Guid CategoryId { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
