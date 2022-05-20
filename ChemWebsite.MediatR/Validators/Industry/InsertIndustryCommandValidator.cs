using ChemWebsite.MediatR.Command;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class InsertIndustryCommandValidator : AbstractValidator<AddIndustryCommand>
    {
        public InsertIndustryCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}
