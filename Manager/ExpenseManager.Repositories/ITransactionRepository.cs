using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository interface for accessing transaction data
    public interface ITransactionRepository
    {
        Task<IEnumerable<TransactionDB>> GetTransactionsByPurseIdAsync(Guid purseId);
        Task<TransactionDB?> GetTransactionByIdAsync(Guid transactionId);
        Task<decimal> BalanceByPurseIdAsync(Guid purseId);
        Task SaveTransactionAsync(TransactionDB transaction);
        Task DeleteTransactionAsync(Guid transactionId);
    }
}
