using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class AddDocumentAuditTrailCommand : IRequest<ServiceResponse<DocumentAuditTrailDto>>
    {
        public Guid DocumentId { get; set; }
        public string OperationName { get; set; }
    }
}
