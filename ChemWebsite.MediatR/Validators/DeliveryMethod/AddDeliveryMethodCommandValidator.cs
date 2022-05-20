using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;

namespace ChemWebsite.MediatR.Validators
{
   public class AddDeliveryMethodCommandValidator : AbstractValidator<AddDeliveryMethodCommand>
    {
        public AddDeliveryMethodCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
          
        }
    }
}