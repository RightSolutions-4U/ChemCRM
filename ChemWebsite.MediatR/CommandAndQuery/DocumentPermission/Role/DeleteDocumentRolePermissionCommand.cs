using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class DeleteDocumentRolePermissionCommand : IRequest<ServiceResponse<DocumentRolePermissionDto>>
    {
        public Guid Id { get; set; }
    }
}
