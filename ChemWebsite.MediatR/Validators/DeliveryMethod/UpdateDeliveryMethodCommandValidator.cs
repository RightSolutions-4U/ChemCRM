using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;
using System;

namespace ChemWebsite.MediatR.Validators
{
   public class UpdateDeliveryMethodCommandValidator : AbstractValidator<UpdateDeliveryMethodCommand>
    {
        public UpdateDeliveryMethodCommandValidator()
        {
            RuleFor(c => c.Id).Must(NotEmptyGuid).WithMessage("Id is required");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
        }

        private bool NotEmptyGuid(Guid p)
        {
            return p != Guid.Empty;
        }
    }
}
