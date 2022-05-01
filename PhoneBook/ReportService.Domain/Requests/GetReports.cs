using MediatR;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Requests
{
    public class GetReports : PagedQuery, IRequest<Response<PagedData<ReportDto>>>
    {
        public override string OrderBy { get; set; } = "Id";
        public string? Location { get; set; }
    }
}
