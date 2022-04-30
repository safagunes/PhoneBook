using ReportService.Domain.Services.Contact.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services.Contact.Dtos
{
    public class ContactInfoDetailDto
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public ContactInfoType Type { get; set; }
        public string Content { get; set; }
    }
}
