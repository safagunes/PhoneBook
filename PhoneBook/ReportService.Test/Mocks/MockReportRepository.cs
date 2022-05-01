using Moq;
using ReportService.Domain.Enums;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using ReportService.Domain.Core.Extentions;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Test.Mocks
{
    public static class MockReportRepository
    {
        public static Mock<IReportRepository> GetReportRepository()
        {
            var reports = new List<Report>
            {
                new Report{
                    Id = Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd"),
                    Status = ReportStatus.Preparing,
                    RequestDate = DateTime.Now,
                    ReportDetail = new ReportDetail
                    {
                        ReportId = Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd"),
                        Location = "istanbul",
                        PeopleCount = 1,
                        PhoneNumberCount = 1
                    }
                },
                new Report{
                    Id = Guid.Parse("71205653-5512-4673-ab47-c9c3cc016ece"),
                    Status = ReportStatus.Done,
                    RequestDate = DateTime.Now,
                    ReportDetail = new ReportDetail
                    {
                        ReportId = Guid.Parse("71205653-5512-4673-ab47-c9c3cc016ece"),
                        Location = "ankara",
                        PeopleCount = 1,
                        PhoneNumberCount = 1
                    }
                },
                new Report{
                    Id = Guid.Parse("3eec7ddc-2bc3-4e67-8f31-983f1eb7bce0"),
                    Status = ReportStatus.Preparing,
                    RequestDate = DateTime.Now,
                    ReportDetail = null
                }
            };

            var mockRepo = new Mock<IReportRepository>();

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Report>())).ReturnsAsync((Report report) =>
            {
                reports.Add(report);
                return report;
            });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Report>())).ReturnsAsync((Report report) =>
            {
                var oldReport = reports.Where(a => a.Id == report.Id).FirstOrDefault();
                oldReport.Status= report.Status;
                return report;
            });

            mockRepo.Setup(r => r.CountAsync(It.IsAny<GetReports>())).ReturnsAsync((GetReports request) =>
            {
                var queryable = reports.AsQueryable();

                if (!string.IsNullOrEmpty(request.Location))
                {
                    queryable = queryable.Where(a => a.ReportDetail != null);
                    queryable = queryable.Where(a => a.ReportDetail.Location.ToLower().Contains(request.Location.ToLower()));
                }

                return queryable.Count();
            });

            mockRepo.Setup(r => r.CountAsync()).ReturnsAsync(reports.Count);


            mockRepo.Setup(r => r.GetAsync(It.IsAny<GetReports>())).ReturnsAsync((GetReports request) =>
            {
                var queryable = reports.AsQueryable();

                if (!string.IsNullOrEmpty(request.Location))
                {
                    queryable = queryable.Where(a => a.ReportDetail != null);
                    queryable = queryable.Where(a => a.ReportDetail.Location.ToLower().Contains(request.Location.ToLower()));
                }
                return queryable.OrderBy(request.OrderBy, request.IsAscending)
                                      .Skip(request.PageIndex.Value * request.PageSize.Value)
                                      .Take(request.PageSize.Value).ToList();
            });

            mockRepo.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Guid reportId) =>
            {
                return reports.FirstOrDefault(a => a.Id == reportId);
            });

            return mockRepo;
        }
    }
}
