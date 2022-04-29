using ContactService.Domain.Models;
using ContactService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.PostgreSql.Repositories
{
    public class PGContactInfoRepository : IContactInfoRepository
    {
        private readonly EFContext _context;
        public PGContactInfoRepository(EFContext context)
        {
            _context = context;
        }
        public async Task<ContactInfo> AddAsync(ContactInfo entity)
        {
            await _context.ContactInfos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task DeleteAsync(Guid contactInfoId)
        {
            var contact = await _context.ContactInfos.FirstOrDefaultAsync(a => a.Id == contactInfoId);
            if (contact != null)
            {
                _context.ContactInfos.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
    }
}
