using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    // Інтерфейс для сервісу зберігання даних, який визначає методи для отримання транзакцій та гаманців
    public interface IStorageService
    {
        public IEnumerable<TransactionDB> GetTransactions(Guid purseId);
        public IEnumerable<PurseDB> GetAllPurses();
    }
}
