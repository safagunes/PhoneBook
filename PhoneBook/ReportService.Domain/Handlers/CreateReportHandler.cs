using AutoMapper;
using MediatR;
using ReportService.Domain.Bus;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Core.ResponseBases;
using ReportService.Domain.Enums;
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
        private readonly IBusPublisher _busPublisher;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportRepository _reportRepository;
        private readonly CreateReportValidator _createReportValidator;

        public CreateReportHandler(
            IBusPublisher busPublisher,
            IUnitOfWork unitOfWork,
            IReportRepository reportRepository,
            CreateReportValidator createReportValidator,
        IMapper mapper)
        {
            _busPublisher = busPublisher;
            _unitOfWork = unitOfWork;
            _reportRepository = reportRepository;
            _createReportValidator = createReportValidator;
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
            //await _unitOfWork.StartTransactionAsync();
            var report = await _reportRepository.AddAsync(new Report
            {
                RequestDate = DateTime.Now,
                Status = ReportStatus.Preparing
            });
            _busPublisher.Publish(new ProcessReport
            {
                ReportId = report.Id,
                Location = request.Location
            }); 
            
            //await _unitOfWork.CommitTransactionAsync();
            return await Task.FromResult(response);
        }
    }
}
