using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class UpdateChemicalCommandValidator : AbstractValidator<UpdateChemicalCommand>
    {
        public UpdateChemicalCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Chemical Name is required");
        }
    }
}
