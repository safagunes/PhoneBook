using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Repositories;
using ReportService.Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Repositories.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context;
        private Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction DbContextTransaction;
        public EFUnitOfWork(EFContext context)
        {
            _context = context;
        }
        public void CommitTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                throw new TransactionException(500,$"There is no transaction to commit");
            }
            DbContextTransaction.Commit();
        }

        public Task CommitTransactionAsync()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                throw new TransactionException(500,$"There is no transaction to commit");
            }
            return DbContextTransaction.CommitAsync();
        }

        public void RollBackTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                throw new TransactionException(500,$"There is no transaction to rollback");
            }
            DbContextTransaction.Rollback();
        }

        public Task RollBackTransactionAsync()
        {
            if (_context.Database.CurrentTransaction == null)
            {
                throw new TransactionException(500,$"There is no transaction to rollback");
            }
            return DbContextTransaction.RollbackAsync();
        }

        public void StartTransaction()
        {
            DbContextTransaction = _context.Database.BeginTransaction();
        }

        public async Task StartTransactionAsync()
        {
            DbContextTransaction = await _context.Database.BeginTransactionAsync();
        }

        public void Dispose()
        {
            if (DbContextTransaction != null)
                DbContextTransaction?.Dispose();
        }
    }
   
}
