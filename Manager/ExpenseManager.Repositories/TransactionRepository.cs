using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository implementation for accessing transaction data
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IStorageContext _storageContext;

        public TransactionRepository(IStorageContext storageContext)
        {
            _storageContext = storageContext;
        }

        public Task<IEnumerable<TransactionDB>> GetTransactionsByPurseIdAsync(Guid purseId)
        {
            return _storageContext.GetTransactionsByPurseIdAsync(purseId);
        }

        public Task<TransactionDB?> GetTransactionByIdAsync(Guid transactionId)
        {
            return _storageContext.GetTransactionByIdAsync(transactionId);
        }

        public Task<decimal> BalanceByPurseIdAsync(Guid purseId)
        {
            return _storageContext.BalanceByPurseIdAsync(purseId);
        }

        public Task SaveTransactionAsync(TransactionDB transaction)
        {
            return _storageContext.SaveTransactionAsync(transaction);
        }

        public Task DeleteTransactionAsync(Guid transactionId)
        {
            return _storageContext.DeleteTransactionAsync(transactionId);
        }

    }
}
