using MediatR;
using ReportService.Domain.Core.Extentions;
using ReportService.Domain.Requests;
using ReportService.Domain.Services;
using ReportService.Domain.Services.Contact.Dtos;
using ReportService.Domain.Services.Contact.Enums;
using ReportService.Domain.Services.Contact.Requests;
using ReportService.Domain.Services.ExcelExport;
using ReportService.Domain.Services.File;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Handlers
{
    public class ProcessReportHandler : IRequestHandler<ProcessReport,Unit>
    {
        private readonly IContactService _contactService;
        private readonly IExcelExportService _excelExportService;
        private readonly IFileService _fileService;
        public ProcessReportHandler(
            IContactService contactService,
            IExcelExportService excelExportService,
            IFileService fileService)
        {
            _contactService = contactService;
            _excelExportService = excelExportService;
            _fileService = fileService;
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

                var datatable = contactDetails.ToList().ToDataTable();

                var content = _excelExportService.Export(datatable);

                await _fileService.SaveAsync($"PhoneBook-{request.ReportId}-{DateTime.Now.ToShortDateString()}", ".xlsx", content);
            }
            return await Task.FromResult(Unit.Value);
        }
    }
}
