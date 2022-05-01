using AutoMapper;
using Moq;
using ReportService.Domain;
using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Enums;
using ReportService.Domain.Handlers;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
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

    public class GetReportHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReportRepository> mockRepo;

        public GetReportHandlerTest()
        {
            mockRepo = MockReportRepository.GetReportRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
        [Fact]
        public async Task Get_Report_By_Id()
        {
            var handler = new GetReportHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetReport { ReportId = Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd") }, CancellationToken.None);
            Assert.True(result.Data != null);
        }

        [Fact]
        public async Task Get_Report_By_Id_ThrowsBusinessException()
        {
            var handler = new GetReportHandler(mockRepo.Object, _mapper);

            Func<Task> act = () => handler.Handle(new GetReport { ReportId = Guid.Parse("24952fa5-512a-426a-a0b1-661b862ef519") }, CancellationToken.None);

            var exception = await Assert.ThrowsAsync<BusinessException>(act);
            Assert.True(exception.Code == (int)ErrorMessage.NotFound);
        }
    }
}
