using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
   public class AddPaymentTermCommandValidator : AbstractValidator<AddPaymentTermCommand>
    {
        public AddPaymentTermCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }
    }
}