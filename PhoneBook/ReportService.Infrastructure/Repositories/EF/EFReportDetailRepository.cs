using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using ReportService.Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Repositories.EF
{
    public class EFReportDetailRepository : IReportDetailRepository
    {
        private readonly EFContext _context;
        public EFReportDetailRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<ReportDetail> AddAsync(ReportDetail entity)
        {
            await _context.ReportDetails.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ReportDetail> GetAsync(Guid reportId)
        {
            return await _context.ReportDetails.FirstOrDefaultAsync(a => a.ReportId == reportId);
        }
    }
}
