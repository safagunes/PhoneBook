using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, List<string>> ValidationErrors;
        public ValidationException(Dictionary<string, List<string>> validationErrors)
        {
            ValidationErrors = validationErrors;
        }
    }
}
