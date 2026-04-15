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

        public IEnumerable<TransactionDB> GetTransactionsByPurseId(Guid purseId)
        {
            return _storageContext.GetTransactionsByPurseId(purseId);
        }

        public decimal BalanceByPurseId(Guid purseId)
        {
            return _storageContext.BalanceByPurseId(purseId);
        }

        public TransactionDB GetTransactionById(Guid transactionId)
        {
            return _storageContext.GetTransactionById(transactionId);
        }

    }
}
