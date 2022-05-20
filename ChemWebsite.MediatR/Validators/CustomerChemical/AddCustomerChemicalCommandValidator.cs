using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class AddCustomerChemicalCommandValidator : AbstractValidator<AddCustomerChemicalCommand>
    {
        public AddCustomerChemicalCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("Customer Id is required");
            RuleFor(c => c.ChemicalIdList).NotEmpty().WithMessage("Chemical Id is required");
        }
    }
}
