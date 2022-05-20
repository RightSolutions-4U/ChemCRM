using ChemWebsite.Data.Dto;
using ChemWebsite.Helper;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ChemWebsite.MediatR.Commands
{
    public class AddDocumentCategoryCommand: IRequest<ServiceResponse<DocumentCategoryDto>>
    {
        [Required(ErrorMessage ="Category Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
