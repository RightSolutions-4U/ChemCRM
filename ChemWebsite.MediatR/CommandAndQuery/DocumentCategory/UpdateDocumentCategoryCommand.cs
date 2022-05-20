using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChemWebsite.MediatR.Commands
{
    public class UpdateDocumentCategoryCommand : IRequest<ServiceResponse<DocumentCategoryDto>>
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
