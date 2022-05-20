using ChemWebsite.MediatR.Commands;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class UpdateDocumentCategoryCommandValidator : AbstractValidator<UpdateDocumentCategoryCommand>
    {
        public UpdateDocumentCategoryCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
