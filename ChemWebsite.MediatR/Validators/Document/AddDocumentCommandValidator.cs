using ChemWebsite.MediatR.Commands;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class AddDocumentCommandValidator : AbstractValidator<AddDocumentCommand>
    {
        public AddDocumentCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Url).NotEmpty().WithMessage("Document is required");
        }
    }
}
