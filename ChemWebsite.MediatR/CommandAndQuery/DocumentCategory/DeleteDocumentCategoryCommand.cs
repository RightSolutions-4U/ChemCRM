using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;

namespace ChemWebsite.MediatR.Commands
{
    public class DeleteDocumentCategoryCommand: IRequest<ServiceResponse<DocumentCategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
