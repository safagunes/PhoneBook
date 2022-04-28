using ContactService.Domain.Models;
using ContactService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact entity);
        Task DeleteAsync(Guid contactId);
        Task<IEnumerable<Contact>> GetAsync(GetContacts request);
        Task<Contact> GetAsync(Guid contactId);
        Task<int> CountAsync(GetContacts request);
        Task<int> CountAsync();
    }
}
