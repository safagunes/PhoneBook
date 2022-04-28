using AutoMapper;
using MediatR;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using ReportService.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Handlers
{
    public class CreateReportHandler : IRequestHandler<CreateReport, Response>
    {
        private readonly IReportRepository _reportRepository;
        private readonly CreateReportValidator _createReportValidator;
        private readonly IMapper _mapper;
        public CreateReportHandler(
            IReportRepository reportRepository,
            CreateReportValidator createReportValidator,
        IMapper mapper)
        {
            _reportRepository = reportRepository;
            _createReportValidator = createReportValidator;
            _mapper = mapper;
        }
        public async Task<Response> Handle(CreateReport request, CancellationToken cancellationToken)
        {
            var response = new Response
            {
                Code = 201
            };
            var validate = _createReportValidator.Validate(request);
            if (validate != null && !validate.IsValid)
            {
                var validations = new Dictionary<string, List<string>>();
                validate.Errors.GroupBy(a => a.PropertyName).ToList().ForEach(a => validations.Add(a.Key, a.Select(b => b.ErrorMessage).ToList()));
                throw new ValidationException(validations);
            }
            await _reportRepository.AddAsync(_mapper.Map<Report>(request));
            return response;
        }
    }
}
