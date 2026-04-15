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
        IEnumerable<TransactionListDTO> GetTransactionsByPurseId(Guid purseId);
        TransactionDetailsDTO GetTransactionDetails(Guid transactionId);

    }
}
