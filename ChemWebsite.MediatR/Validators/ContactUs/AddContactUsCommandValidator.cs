using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class AddContactUsCommandValidator : AbstractValidator<AddContactUsCommand>
    {
        public AddContactUsCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
