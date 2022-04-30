using ReportService.Domain.Services.Contact.Dtos;
using ReportService.Domain.Services.Contact.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services
{
    public interface IContactService
    {
        Task<IEnumerable<ContactDto>> GetContactsAsync(GetContacts request);
        Task<ContactDetailDto> GetContactDetailAsync(GetContact request);
    }
}
