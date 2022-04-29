using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using ContactService.Domain.Requests;
using ContactService.Domain.Core.Extentions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ContactService.Infrastructure.PostgreSql.Repositories
{
    public class PGContactRepository : IContactRepository
    {
        private readonly EFContext _context;
        public PGContactRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<Contact> AddAsync(Contact entity)
        {
            await _context.Contacts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(GetContacts request)
        {
            var queryable = _context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
                queryable = queryable.Where(a => a.FirstName.ToLowerInvariant().Contains(request.FirstName.ToLowerInvariant()));
            if (!string.IsNullOrEmpty(request.LastName))
                queryable = queryable.Where(a => a.LastName.ToLowerInvariant().Contains(request.LastName.ToLowerInvariant()));
            if (!string.IsNullOrEmpty(request.Company))
                queryable = queryable.Where(a => a.Company.ToLowerInvariant().Contains(request.Company.ToLowerInvariant()));

            return await queryable.CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Contacts.CountAsync();
        }

        public async Task DeleteAsync(Guid contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(a => a.Id == contactId);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Contact>> GetAsync(GetContacts request)
        {
            var queryable = _context.Contacts.AsQueryable();

            if (!string.IsNullOrEmpty(request.FirstName))
                queryable = queryable.Where(a => a.FirstName.ToLowerInvariant().Contains(request.FirstName.ToLowerInvariant()));
            if (!string.IsNullOrEmpty(request.LastName))
                queryable = queryable.Where(a => a.LastName.ToLowerInvariant().Contains(request.LastName.ToLowerInvariant()));
            if (!string.IsNullOrEmpty(request.Company))
                queryable = queryable.Where(a => a.Company.ToLowerInvariant().Contains(request.Company.ToLowerInvariant()));

            return await queryable.OrderBy(request.OrderBy, request.IsAscending)
                                  .Skip(request.PageIndex.Value* request.PageSize.Value)
                                  .Take(request.PageSize.Value).ToListAsync();
        }

        public async Task<Contact> GetAsync(Guid contactId)
        {
            return await _context.Contacts.Include(a=>a.ContactInfos).FirstOrDefaultAsync(a => a.Id == contactId);
        }
    }
}
