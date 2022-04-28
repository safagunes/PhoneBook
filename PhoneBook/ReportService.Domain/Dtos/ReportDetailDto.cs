using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Dtos
{
    public class ReportDetailDto
    {
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }
    }
}
