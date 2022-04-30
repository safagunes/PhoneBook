using ReportService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Models
{
    public class Report
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime RequestDate { get; set; }
        public  ReportStatus Status { get; set; }
        public ReportDetail ReportDetail { get; set; }
    }
}
