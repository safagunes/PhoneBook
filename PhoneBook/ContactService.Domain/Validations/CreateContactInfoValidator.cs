using ContactService.Domain.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Validations
{
    public class CreateContactInfoValidator : AbstractValidator<CreateContactInfo>
    {
        public CreateContactInfoValidator()
        {
            RuleFor(x => x.ContactId).NotNull().WithMessage("ContactId is required");
            RuleFor(x => x.Type).NotNull().WithMessage("Type is required");
            RuleFor(x => x.Content).NotNull().WithMessage("Content is required");
            //RuleFor(x => x.LastName).NotNull().WithMessage("LastName is required");
            //RuleFor(x => x.Company).NotNull().WithMessage("Company is required");
        }

    }
}
