using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services.Contact.Dtos
{
    public class ContactDetailDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }

        public IEnumerable<ContactInfoDetailDto> ContactInfos { get; set; }
    }
}
