using Moq;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Test.Mocks
{
    public static class MockReportDetailRepository
    {
        public static Mock<IReportDetailRepository> GetReportDetailRepository()
        {
            var reportDetails = new List<ReportDetail>
            {
                
                    new ReportDetail
                    {
                        ReportId = Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd"),
                        Location = "istanbul",
                        PeopleCount = 1,
                        PhoneNumberCount = 1
                    }
                    ,new ReportDetail
                    {
                        ReportId = Guid.Parse("71205653-5512-4673-ab47-c9c3cc016ece"),
                        Location = "ankara",
                        PeopleCount = 1,
                        PhoneNumberCount = 1
                    }
            };

            var mockRepo = new Mock<IReportDetailRepository>();

            mockRepo.Setup(r => r.AddAsync(It.IsAny<ReportDetail>())).ReturnsAsync((ReportDetail reportDetail) =>
            {
                reportDetails.Add(reportDetail);
                return reportDetail;
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid reportId) =>
            {
                return reportDetails.FirstOrDefault(a => a.ReportId == reportId);
            });

            return mockRepo;
        }
    }
}
