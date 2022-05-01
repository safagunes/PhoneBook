using AutoMapper;
using MediatR;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Dtos;
using ReportService.Domain.Enums;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Handlers
{
    public class GetReportsHandler : IRequestHandler<GetReports, Response<PagedData<ReportDto>>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportsHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }
        public async Task<Response<PagedData<ReportDto>>> Handle(GetReports request, CancellationToken cancellationToken)
        {

            var response = new Response<PagedData<ReportDto>>();
            var reports = await _reportRepository.GetAsync(request);

            response.Data = new PagedData<ReportDto>
            {
                Items = _mapper.Map<IEnumerable<ReportDto>>(reports),
                Count = await _reportRepository.CountAsync(request),
                TotalCount = await _reportRepository.CountAsync()
            };

            return response;
        }
    }
}
