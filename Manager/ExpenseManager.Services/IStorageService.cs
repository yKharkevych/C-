using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    public interface IStorageService
    {
        public IEnumerable<TransactionDB> GetTransactions(Guid purseId);
        public IEnumerable<PurseDB> GetAllPurses();
    }
}
