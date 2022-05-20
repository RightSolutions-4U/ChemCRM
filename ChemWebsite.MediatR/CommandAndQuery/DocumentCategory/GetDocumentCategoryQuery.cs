using ChemWebsite.Data.Dto;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Queries
{
    public class GetDocumentCategoryQuery : IRequest<DocumentCategoryDto>
    {
        public Guid Id { get; set; }
    }
}
