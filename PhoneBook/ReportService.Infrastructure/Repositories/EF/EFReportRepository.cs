using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Models;
using ReportService.Domain.Repositories;
using ReportService.Domain.Requests;
using ReportService.Infrastructure.Data.EF;
using ReportService.Domain.Core.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Repositories.EF
{
    public class EFReportRepository : IReportRepository
    {
        private readonly EFContext _context;
        public EFReportRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<Report> AddAsync(Report entity)
        {
            await _context.Reports.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(GetReports request)
        {
            var queryable = _context.Reports.AsQueryable();
            if (!string.IsNullOrEmpty(request.Location))
            {
                queryable = queryable.Where(a => a.ReportDetail != null);
                queryable = queryable.Where(a => a.ReportDetail.Location.ToLower().Contains(request.Location.ToLower()));
            }
            return await queryable.CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Reports.CountAsync();
        }

        public async Task<IEnumerable<Report>> GetAsync(GetReports request)
        {
            var queryable = _context.Reports.AsQueryable();
            if (!string.IsNullOrEmpty(request.Location))
            {
                queryable = queryable.Where(a => a.ReportDetail != null);
                queryable = queryable.Where(a => a.ReportDetail.Location.ToLower().Contains(request.Location.ToLower()));
            }
            return await queryable.OrderBy(request.OrderBy, request.IsAscending)
                                  .Skip(request.PageIndex.Value * request.PageSize.Value)
                                  .Take(request.PageSize.Value).ToListAsync();
        }

        public async Task<Report> GetAsync(Guid reportId)
        {
            return await _context.Reports.Include(a => a.ReportDetail).FirstOrDefaultAsync(a => a.Id == reportId);
        }

        public async Task<Report> UpdateAsync(Report entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
