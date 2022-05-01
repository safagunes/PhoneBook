using ContactService.Domain.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Validations
{
    public class DeleteContactInfoValidator : AbstractValidator<DeleteContactInfo>
    {
        public DeleteContactInfoValidator()
        {
            RuleFor(x => x.ContactInfoId).NotNull().WithMessage("ContactInfoId is required");
        }
    }
}
