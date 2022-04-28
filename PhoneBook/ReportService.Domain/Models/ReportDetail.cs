using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Models
{
    public class ReportDetail
    {
        public Guid ReportId { get; set; }
        public string Location { get; set; }
        public int PeopleCount { get; set; }
        public int PhoneNumberCount { get; set; }
        public Report Report { get; set; }
    }
}
