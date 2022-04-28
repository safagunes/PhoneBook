using ContactService.Domain.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Validations
{
    public class CreateContactValidator:AbstractValidator<CreateContact> 
    {
        public CreateContactValidator()
        {
            RuleFor(x => x.FirstName).NotNull().WithMessage("FirstName is required");
            //RuleFor(x => x.LastName).NotNull().WithMessage("LastName is required");
            //RuleFor(x => x.Company).NotNull().WithMessage("Company is required");
        }
       
    }
}
