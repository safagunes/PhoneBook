using AutoMapper;
using Moq;
using ReportService.Domain;
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
    public class GetReportsHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IReportRepository> mockRepo;

        public GetReportsHandlerTest()
        {
            mockRepo = MockReportRepository.GetReportRepository();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

        }

        [Fact]
        public async Task Get_Reports()
        {
            var handler = new GetReportsHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetReports { }, CancellationToken.None);
            Assert.Equal(3, result.Data.Count);
        }

        [Fact]
        public async Task Get_Reports_By_Location()
        {
            var handler = new GetReportsHandler(mockRepo.Object, _mapper);
            var result = await handler.Handle(new GetReports
            {
                Location = "istanbul"
            }, CancellationToken.None);
            Assert.Equal(1, result.Data.Count);
        }
    }
}
