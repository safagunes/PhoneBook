using ContactService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Repositories
{
    public interface IContactInfoRepository
    {
        Task<ContactInfo> AddAsync(ContactInfo entity);
        Task DeleteAsync(Guid contactInfoId);
      
    }
}
