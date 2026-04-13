using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace Manager.ExpenseManager.UIModels
{
    public class PurseUI
    {
        private readonly IStorageService _storage;
        private PurseDB _db;
        private string _name;
        private Currency _currency;
        private decimal _startBalance;
        private List<TransactionUI> _transactions;

        public Guid? Id
        {
            get => _db?.Id;
        }
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public Currency Currency
        {
            get => _currency;
        }
        public decimal StartBalance
        {
            get => _startBalance;
        }
        public IReadOnlyList<TransactionUI> Transactions
        {
            get => _transactions;
        }
        public decimal Balance
        {
            get => _startBalance + (Transactions?.Sum(t => t.Amount) ?? 0);
        }

        // Властивість для відображення балансу у вигляді рядка
        public string BalanceDesc
        {
            get
            {
                var transactionsSum = Transactions?.Sum(t => t.Amount);

                if (transactionsSum == null)
                    return "Balance Not Loaded";

                return $"{_startBalance + transactionsSum.Value} {Currency}";
            }
        }

        public PurseUI(IStorageService storage)
        {
            _transactions = new List<TransactionUI>();
            _storage = storage;
        }

        public PurseUI(IStorageService storage, PurseDB db) 
        {
            _storage = storage;
            _db = db;
            _name = db.Name;
            _currency = db.Currency;
            _startBalance = db.StartBalance;
        }

        public void SaveChangesToDB()
        {
            if (_db == null)
            {
                _db = new PurseDB(_name, _currency, _startBalance);
            }
            else
            {
                _db.Name = _name;
            }
        }

        public void LoadTransactions()
        {
            if (Id == null || _transactions != null)
                return;
            _transactions = new List<TransactionUI>();
            foreach (var transactionDB in _storage.GetTransactions(Id.Value))
            {
                _transactions.Add(new TransactionUI(transactionDB));
            }
        }

        override public string ToString()
        {
            return $"{Name} ({Currency}), Balance: {Balance} {Currency}";
        }
    }
}
