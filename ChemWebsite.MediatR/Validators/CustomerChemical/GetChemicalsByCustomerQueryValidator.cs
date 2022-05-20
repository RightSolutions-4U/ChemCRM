using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators.CustomerChemical
{
    public class GetChemicalsByCustomerQueryValidator : AbstractValidator<GetChemicalsByCustomerQuery>
    {
        public GetChemicalsByCustomerQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Customer Id is required");
        }
    }
}
