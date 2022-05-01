using Moq;
using ReportService.Domain.Enums;
using ReportService.Domain.Handlers;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using ReportService.Domain.Services;
using ReportService.Domain.Services.ExcelExport;
using ReportService.Domain.Services.File;
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
    public class ProcessReportHandlerTest
    {
        
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IReportRepository> _reportRepository;
        private readonly Mock<IReportDetailRepository> _reportDetailRepository;
        private readonly Mock<IContactService> _contactService;
        private readonly Mock<IExcelExportService> _excelExportService;
        private readonly Mock<IFileService> _fileService;

        public ProcessReportHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _reportRepository = MockReportRepository.GetReportRepository();
            _reportDetailRepository = MockReportDetailRepository.GetReportDetailRepository();
            _contactService = MockContactService.GetContactService();
            _excelExportService = new Mock<IExcelExportService>();
            _fileService = new Mock<IFileService>();
        }

        [Fact]
        public async Task Process_Report()
        {
            var handler = new ProcessReportHandler(_unitOfWork.Object,_reportRepository.Object, _reportDetailRepository.Object, _contactService.Object, _excelExportService.Object, _fileService.Object);
            var result = await handler.Handle(new ProcessReport
            {
                ReportId = Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd"),
                Location = "istanbul"
            }, CancellationToken.None);

            var reports = await _reportRepository.Object.GetAsync(Guid.Parse("0fc1efe9-cf3e-4922-93f3-0b1489e0fafd"));
            Assert.True(reports.Status==ReportStatus.Done);
        }
    }
}
