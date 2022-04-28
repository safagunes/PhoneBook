using FluentValidation;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Validations
{
    public class CreateReportValidator:AbstractValidator<CreateReport>
    {
        public CreateReportValidator()
        {
            RuleFor(x => x.Location).NotNull().WithMessage("Location is required");
        }
    }
}
