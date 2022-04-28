using ReportService.Domain.Models;
using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Repositories
{
    public interface IReportRepository
    {
        Task<Report> AddAsync(Report entity);
        Task<IEnumerable<Report>> GetAsync(GetReports request);
        Task<Report> GetAsync(Guid reportId);
        Task<int> CountAsync(GetReports request);
        Task<int> CountAsync();
    }
}
