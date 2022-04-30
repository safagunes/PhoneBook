using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Requests
{
    public class ProcessReport : IRequest<Unit>
    {
        public Guid ReportId { get; set; }
        public string Location { get; set; }
    }
}
