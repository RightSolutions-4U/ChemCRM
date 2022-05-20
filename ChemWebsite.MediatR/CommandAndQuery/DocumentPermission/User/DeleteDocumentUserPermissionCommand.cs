using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class DeleteDocumentUserPermissionCommand : IRequest<ServiceResponse<DocumentUserPermissionDto>>
    {
        public Guid Id { get; set; }
    }
}
