using ContactService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Dtos
{
    public class ContactInfoDto
    {
        public ContactInfoType Type { get; set; }
        public string Content { get; set; }
    }
}
