using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Storage
{
    // Interface for storage context to abstract data access
    public interface IStorageContext
    {
        IEnumerable<PurseDB> GetPurses();
        PurseDB GetPurse(Guid id);
        IEnumerable<TransactionDB> GetTransactionsByPurseId(Guid purseId);
        TransactionDB GetTransactionById(Guid transactionId);
        decimal BalanceByPurseId(Guid purseId);
    }
}
