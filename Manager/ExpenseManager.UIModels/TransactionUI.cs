using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Manager.ExpenseManager.UIModels
{
    public class TransactionUI
    {
        private TransactionDB _db;
        private Guid _purseId;
        private decimal _amount;
        private Category _category;
        private DateTime _date;
        private string _description;
        private Boolean _isExpense;

        public Guid? Id
        {
            get => _db?.Id;
        }
        public Guid PurseId
        {
            get => _purseId;
        }
        public decimal Amount
        {
            get => _amount;
            set => _amount = value;
        }
        public Category Category
        {
            get => _category;
            set => _category = value;
        }
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public Boolean IsExpense
        {
            get => _isExpense;
        }

        public string InOutCome
        {
            get
            {
                if (IsExpense) return "";
                return "+";
            }
        }

        public string DateDisplay => Date.ToString("MMMM dd, yyyy", new CultureInfo("en-US"));

        public TransactionUI(Guid purseId)
        {
            _purseId = purseId;
        }

        public TransactionUI(TransactionDB db)
        {
            _db = db;
            _purseId = _db.PurseId;
            _amount = _db.Amount;
            _category = _db.Category;
            _date = _db.Date;
            _description = _db.Description;
            IsExpenseTransaction();
        }

        // Метод для визначення, чи є транзакція витратною
        private void IsExpenseTransaction()
        {
            _isExpense = _amount < 0;
        }

        public void SaveChangesToDB()
        {
            if (_db == null)
            {
                _db = new TransactionDB(_purseId, _amount, _category, _date, _description);
            }
            else
            {
                _db.Amount = _amount;
                _db.Category = _category;
                _db.Date = _date;
                _db.Description = _description;
            }
        }

        override public string ToString()
        {
            return $"{Date.ToShortDateString()} - {Category} - {Amount} - {Description}";
        }
    }
}
