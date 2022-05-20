using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.Commands
{
    public class AddDocumentRolePermissionCommand : IRequest<ServiceResponse<DocumentRolePermissionDto>>
    {
        public ICollection<DocumentRolePermissionDto> DocumentRolePermissions { get; set; }
    }
}
