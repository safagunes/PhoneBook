using MediatR;
using ReportService.Domain.Core.Extentions;
using ReportService.Domain.Dtos;
using ReportService.Domain.Enums;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using ReportService.Domain.Services;
using ReportService.Domain.Services.Contact.Dtos;
using ReportService.Domain.Services.Contact.Enums;
using ReportService.Domain.Services.Contact.Requests;
using ReportService.Domain.Services.ExcelExport;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Handlers
{
    public class ProcessReportHandler : IRequestHandler<ProcessReport, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportRepository _reportRepository;
        private readonly IReportDetailRepository _reportDetailRepository;
        private readonly IContactService _contactService;
        private readonly IExcelExportService _excelExportService;
        public ProcessReportHandler(
            IUnitOfWork unitOfWork,
            IReportRepository reportRepository,
            IReportDetailRepository reportDetailRepository,
            IContactService contactService,
            IExcelExportService excelExportService)
        {
            _unitOfWork = unitOfWork;
            _reportRepository = reportRepository;
            _reportDetailRepository = reportDetailRepository;
            _contactService = contactService;
            _excelExportService = excelExportService;
        }
        public async Task<Unit> Handle(ProcessReport request, CancellationToken cancellationToken)
        {
            //Todo Excel oluştuma kodları.
            var contacts = await _contactService.GetContactsAsync(new GetContacts());
            if (contacts != null && contacts.Any())
            {
                var contactDetails = new ConcurrentBag<ContactDetailDto>();
                await contacts.ParallelForEachAsync(async (contact) =>
                {
                    var contactDetail = await _contactService.GetContactDetailAsync(new GetContact
                    {
                        ContactId = contact.Id
                    });
                    if (contactDetail != null && contactDetail.ContactInfos.Any(a => a.Type == ContactInfoType.Location && a.Content.ToLower().Contains(request.Location.ToLower())))
                    {
                        contactDetails.Add(contactDetail);
                    }
                }, 10);


                var reportDetail = new ReportDetail
                {
                    ReportId = request.ReportId,
                    Location = request.Location,
                    PeopleCount = contactDetails.Count,
                    PhoneNumberCount = contactDetails.SelectMany(a => a.ContactInfos.Where(a => a.Type == ContactInfoType.PhoneNumber).Select(a => a)).Count()
                };

                var datatable = new List<ReportDetail> { reportDetail }.ToDataTable();

                _excelExportService.Export(datatable, $"PhoneBook-{request.ReportId}-{DateTime.Now:yyyy-MM-dd}.xlsx");

                //await _unitOfWork.StartTransactionAsync();
                await _reportDetailRepository.AddAsync(reportDetail);

                var report = await _reportRepository.GetAsync(request.ReportId);
                if (report != null)
                {
                    report.Status = ReportStatus.Done;
                }

                await _reportRepository.UpdateAsync(report);
                //await _unitOfWork.CommitTransactionAsync();
            }
            return await Task.FromResult(Unit.Value);
        }
    }
}
