using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class DeleteCustomerChemicalCommandValidator : AbstractValidator<DeleteCustomerChemicalCommand>
    {
        public DeleteCustomerChemicalCommandValidator()
        {
            RuleFor(c => c.CustomerId).NotEmpty().WithMessage("Customer Id is required");
            RuleFor(c => c.ChemicalId).NotEmpty().WithMessage("Chemical Id is required");
        }
    }
}
