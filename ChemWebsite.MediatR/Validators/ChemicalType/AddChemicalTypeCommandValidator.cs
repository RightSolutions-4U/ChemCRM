using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class AddChemicalTypeCommandValidator : AbstractValidator<AddChemicalTypeCommand>
    {
        public AddChemicalTypeCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Chemical Type Name is required");
        }
    }
}
