using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Repositories
{
    // Repository interface for accessing transaction data
    public interface ITransactionRepository
    {
        IEnumerable<TransactionDB> GetTransactionsByPurseId(Guid purseId);
        TransactionDB GetTransactionById(Guid transactionId);
        decimal BalanceByPurseId(Guid purseId);
    }
}
