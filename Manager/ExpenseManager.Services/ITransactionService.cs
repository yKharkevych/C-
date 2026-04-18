using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.DBModels.Transactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    // Service interface for managing transactions
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionListDTO>> GetTransactionsByPurseIdAsync(Guid purseId);
        Task<TransactionDetailsDTO?> GetTransactionDetailsAsync(Guid transactionId);
        Task<TransactionEditDTO?> GetTransactionForEditAsync(Guid transactionId);
        Task CreateTransactionAsync(TransactionCreateDTO createDto);
        Task UpdateTransactionAsync(TransactionEditDTO editDto);
        Task DeleteTransactionAsync(Guid transactionId);
    }
}
