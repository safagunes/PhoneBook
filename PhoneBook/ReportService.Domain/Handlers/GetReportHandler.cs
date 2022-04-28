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
    public class GetReportHandler : IRequestHandler<GetReport, Response<ReportDetailDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        public GetReportHandler(IReportRepository reportRepository, IMapper mapper)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
        }

        public async Task<Response<ReportDetailDto>> Handle(GetReport request, CancellationToken cancellationToken)
        {
            var response = new Response<ReportDetailDto>();

            var contact = await _reportRepository.GetAsync(request.ReportId);

            if (contact == null)
                throw new BusinessException(ErrorMessage.NotFound);

            response.Data = _mapper.Map<ReportDetailDto>(contact);
            return response;
        }
    }
}
