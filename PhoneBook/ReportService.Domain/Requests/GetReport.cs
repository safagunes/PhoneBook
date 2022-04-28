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
    public class GetReport : IRequest<Response<ReportDetailDto>>
    {
        public Guid ReportId { get; set; }
    }
}
