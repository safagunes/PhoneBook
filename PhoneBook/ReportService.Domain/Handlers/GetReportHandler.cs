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
    public class GetReportHandler : IRequestHandler<GetReport, Response<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<Response<ReportDto>> Handle(GetReport request, CancellationToken cancellationToken)
        {
            var response = new Response<ReportDto>();

            var report = await _reportRepository.GetAsync(request.ReportId);

            if (report == null)
                throw new BusinessException(ErrorMessage.NotFound);

            response.Data = _mapper.Map<ReportDto>(report);
            return response;
        }
    }
}
