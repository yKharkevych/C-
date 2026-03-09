using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    public class StorageService
    {
        private List<PurseDB> _purses;
        private  List<TransactionDB> _transactions;

        private void LoadData()
        {
            if(_purses != null && _transactions != null)
            {
                return; 
            }
            _purses = FakeStorage.Purses.ToList();
            _transactions = FakeStorage.Transactions.ToList();
        }

        // Метод для отримання всіх гаманців
        public IEnumerable<PurseDB> GetAllPurses()
        {
            LoadData();
            return _purses.ToList();
        }

        // Метод для отримання транзакцій за ID гаманця
        public IEnumerable<TransactionDB> GetTransactions(Guid purseId) {
            LoadData();
            return _transactions.Where(t => t.PurseId == purseId).ToList();
        }
    }
}
