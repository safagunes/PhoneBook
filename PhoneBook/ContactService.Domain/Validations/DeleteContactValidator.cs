using ContactService.Domain.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Validations
{
    public class DeleteContactValidator:AbstractValidator<DeleteContact>
    {
        public DeleteContactValidator()
        {
            RuleFor(x => x.ContactId).NotNull().WithMessage("ContactId is required");
        }
    }
}
