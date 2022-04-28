using ContactService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Repositories
{
    public interface IContactRepository
    {
        Task<Contact> AddAsync(Contact entity);
    }
}
