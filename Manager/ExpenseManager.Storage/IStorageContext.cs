using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Storage
{
    // Interface for storage context to abstract data access
    public interface IStorageContext
    {
        // Purses
        Task<IEnumerable<PurseDB>> GetPursesAsync();
        Task<PurseDB?> GetPurseAsync(Guid id);
        Task SavePurseAsync(PurseDB purse);
        Task DeletePurseAsync(Guid purseId);

        // Transactions
        Task<IEnumerable<TransactionDB>> GetTransactionsByPurseIdAsync(Guid purseId);
        Task<TransactionDB?> GetTransactionByIdAsync(Guid transactionId);
        Task SaveTransactionAsync(TransactionDB transaction);
        Task DeleteTransactionAsync(Guid transactionId);

        Task<decimal> BalanceByPurseIdAsync(Guid purseId);
    }
}
