using ChemWebsite.MediatR.Commands;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class AddDocumentCategoryCommandValidator : AbstractValidator<AddDocumentCategoryCommand>
    {
        public AddDocumentCategoryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
