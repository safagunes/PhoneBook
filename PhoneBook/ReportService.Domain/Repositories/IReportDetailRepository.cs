using ReportService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Repositories
{
    public interface IReportDetailRepository
    {
        Task<ReportDetail> AddAsync(ReportDetail entity);
        Task<ReportDetail> GetAsync(Guid reportId);
    }
}
