using MediatR;
using ReportService.Domain.Core.ResponseBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Requests
{
    public class CreateReport:IRequest<Response>
    {
        public string? Location { get; set; }
    }
}
