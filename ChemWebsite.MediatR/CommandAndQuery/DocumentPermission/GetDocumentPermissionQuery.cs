using ChemWebsite.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;

namespace ChemWebsite.MediatR.Queries
{
    public class GetDocumentPermissionQuery : IRequest<List<DocumentPermissionDto>>
    {
        public Guid DocumentId { get; set; }
    }

}
