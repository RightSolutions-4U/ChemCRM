using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.Commands
{
    public class AddDocumentUserPermissionCommand : IRequest<ServiceResponse<DocumentUserPermissionDto>>
    {
        public ICollection<DocumentUserPermissionDto> DocumentUserPermissions { get; set; }
    }
}
