using AutoMapper;
using Moq;
using ReportService.Domain;
using ReportService.Domain.Bus;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Handlers;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using ReportService.Domain.Validations;
using ReportService.Test.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ReportService.Test.Handlers
{
    public class CreateReportHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReportRepository> mockRepo;
        private readonly CreateReportValidator _createReportValidator;
        private readonly Mock<IBusPublisher> _busPublisher;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateReportHandlerTest()
        {

            mockRepo = MockReportRepository.GetReportRepository();
            _busPublisher = new Mock<IBusPublisher>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _createReportValidator = new CreateReportValidator();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Create_Report()
        {
            var handler = new CreateReportHandler(_busPublisher.Object, _unitOfWork.Object, mockRepo.Object, _createReportValidator, _mapper);
            var result = await handler.Handle(new CreateReport
            {
                Location="istanbul"
            }, CancellationToken.None);

            var reports = await mockRepo.Object.GetAsync(new GetReports {});
            Assert.Equal(4, reports.Count());
        }
        [Fact]
        public async Task Create_Report_ThrowsValidationException()
        {
            var handler = new CreateReportHandler(_busPublisher.Object, _unitOfWork.Object, mockRepo.Object, _createReportValidator, _mapper);

            Func<Task> act = () => handler.Handle(new CreateReport
            {
                //Location = "istanbul"
            }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<ValidationException>(act);
            Assert.True(exception.ValidationErrors.ContainsKey("Location"));
        }
    }
}
