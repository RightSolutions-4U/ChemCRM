using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class UpdateChemicalTypeCommandValidator : AbstractValidator<UpdateChemicalTypeCommand>
    {
        public UpdateChemicalTypeCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Chemical Type Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Chemical Type Name is required");
        }
    }
}
