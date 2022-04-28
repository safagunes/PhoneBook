using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Enums
{
    public enum ErrorMessage
    {
        [Description("Not Found")]
        NotFound = 404,
        [Description("Internal Server Error")]
        InternalServerError = 500,
        [Description("Required Fields")]
        Validation = 422
    }
}
