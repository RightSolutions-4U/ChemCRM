using ChemWebsite.MediatR.CommandAndQuery;
using FluentValidation;
using System;

namespace ChemWebsite.MediatR.Validators
{
   public class DeleteDeliveryMethodCommandValidator : AbstractValidator<DeleteDeliveryMethodCommand>
    {
        public DeleteDeliveryMethodCommandValidator()
        {
            RuleFor(c => c.Id).Must(NotEmptyGuid).WithMessage("Id is required");

        }

        private bool NotEmptyGuid(Guid p)
        {
            return p != Guid.Empty;
        }
    }
}