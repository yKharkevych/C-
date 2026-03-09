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
        private PurseDB _db;
        private string _name;
        private Currency _currency;
        private decimal _startBalance;
        private readonly List<TransactionUI> _transactions;

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
            get => _startBalance + Transactions.Sum(t => t.Amount);
        }

        public PurseUI()
        {
            _transactions = new List<TransactionUI>();
        }

        public PurseUI(PurseDB db) : this()
        {
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

        public void LoadTransactions(StorageService storage)
        {
            if (Id == null || _transactions.Count() > 0)
                return;
            foreach (var transactionDB in storage.GetTransactions(Id.Value))
            {
                _transactions.Add(new TransactionUI(transactionDB));
            }
        }

        override public string ToString()
        {
            return $"{Name} ({Currency}), Баланс: {Balance}";
        }
    }
}
