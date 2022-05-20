using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators.Chemical
{
    public class AddChemicalCommandValidator : AbstractValidator<AddChemicalCommand>
    {
        public AddChemicalCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Chemical Name is required");
        }
    }
}
