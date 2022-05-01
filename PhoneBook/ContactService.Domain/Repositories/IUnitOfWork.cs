using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task StartTransactionAsync();
        void StartTransaction();
        Task CommitTransactionAsync();
        void CommitTransaction();
        Task RollBackTransactionAsync();
        void RollBackTransaction();
    }
}
