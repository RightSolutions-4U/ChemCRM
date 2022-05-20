using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
    public class GetCustomersByChemicalQueryValidator : AbstractValidator<GetCustomersByChemicalQuery>
    {
        public GetCustomersByChemicalQueryValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Chemical Id is required");
        }
    }
}
