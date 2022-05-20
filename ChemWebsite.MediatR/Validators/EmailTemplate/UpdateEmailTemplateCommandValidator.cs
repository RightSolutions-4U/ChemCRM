using FluentValidation;
using ChemWebsite.MediatR.CommandAndQuery;

namespace ChemWebsite.MediatR.Validators
{
    public class UpdateEmailTemplateCommandValidator : AbstractValidator<UpdateEmailTemplateCommand>
    {
        public UpdateEmailTemplateCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.Subject).NotEmpty().WithMessage("Subject is required");
            RuleFor(c => c.Body).NotEmpty().WithMessage("Body is required");
        }
    }
}
