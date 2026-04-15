using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Manager.ExpenseManager.Storage
{
    // In-memory implementation of the storage context for testing and development purposes
    public class InMemoryStorageContext : IStorageContext
    {
        private record PurseRecord(Guid Id, string Name, Currency Currency, decimal StartBalance);
        private record TransactionRecord(Guid Id, Guid PurseId, decimal Amount, Category Category, DateTime Date, string Description);


        private static readonly List<PurseRecord> _purses = new List<PurseRecord>();
        private static readonly List<TransactionRecord> _transactions = new List<TransactionRecord>();

        static InMemoryStorageContext()
        {
            #region MockStoragePopulation   
            var cash = new PurseRecord(Guid.NewGuid(), "Готівка", Currency.UAH, 1000);
            var monoUAH = new PurseRecord(Guid.NewGuid(), "Монобанк", Currency.UAH, 12645);
            var monoUSD = new PurseRecord(Guid.NewGuid(), "Монобанк", Currency.USD, 580);
            var cashPLN = new PurseRecord(Guid.NewGuid(), "Готівка", Currency.PLN, 1730.50m);
            _purses = new List<PurseRecord> { cash, monoUAH, monoUSD, cashPLN };

            var t1 = new TransactionRecord(Guid.NewGuid(), cash.Id, -50, Category.Cafe, new DateTime(2026, 3, 7, 15, 34, 56), "Купівля кави");
            var t2 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, 25000, Category.Salary, new DateTime(2026, 3, 1, 14, 25, 46), "Дохід з проєкту");
            var t3 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, -1200, Category.Clothes, new DateTime(2026, 3, 4, 13, 36, 23), "Нові джинси");
            var t4 = new TransactionRecord(Guid.NewGuid(), monoUSD.Id, -18.5m, Category.Cafe, new DateTime(2026, 3, 1, 15, 46, 21), "Обід");
            var t5 = new TransactionRecord(Guid.NewGuid(), cashPLN.Id, -200, Category.Entertainment, new DateTime(2026, 3, 2, 18, 45, 21), "Квитки в кіно");
            var t6 = new TransactionRecord(Guid.NewGuid(), monoUSD.Id, 300, Category.Other, new DateTime(2026, 3, 5, 12, 54, 47), "Продаж старого телефону");
            var t7 = new TransactionRecord(Guid.NewGuid(), cashPLN.Id, -368, Category.Products, new DateTime(2026, 3, 1, 21, 43, 21), "");
            var t8 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, -500, Category.Transport, new DateTime(2026, 3, 5, 8, 40, 11), "Таксі");
            var t9 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, -1500, Category.House, new DateTime(2026, 3, 3, 13, 36, 23), "Комунальні послуги");
            var t10 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, -17500, Category.House, new DateTime(2026, 3, 4, 10, 15, 47), "Оренда");
            var t11 = new TransactionRecord(Guid.NewGuid(), monoUSD.Id, 650, Category.Salary, new DateTime(2026, 3, 3, 15, 34, 56), "Фріланс");
            var t12 = new TransactionRecord(Guid.NewGuid(), monoUSD.Id, -200, Category.Other, new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти");
            var t13 = new TransactionRecord(Guid.NewGuid(), monoUAH.Id, 8600, Category.Other, new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти");
            _transactions = new List<TransactionRecord> { t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13 };
            #endregion
        }

        public IEnumerable<PurseDB> GetPurses()
        {
            foreach (var purse in _purses)
            {
                yield return new PurseDB(purse.Id, purse.Name, purse.Currency, purse.StartBalance);
            }
        }

        public PurseDB GetPurse(Guid id)
        {
            var purse = _purses.FirstOrDefault(p => p.Id == id);
            return purse is null ? null : new PurseDB(purse.Id, purse.Name, purse.Currency, purse.StartBalance);
        }

        public IEnumerable<TransactionDB> GetTransactionsByPurseId(Guid purseId)
        {
            return _transactions.Where(t => t.PurseId == purseId).Select(transaction => new TransactionDB(transaction.Id, transaction.PurseId, transaction.Amount, transaction.Category, transaction.Date, transaction.Description));
        }

        public decimal BalanceByPurseId(Guid purseId)
        {
            var purse = _purses.FirstOrDefault(p => p.Id == purseId) ?? throw new ArgumentException();
            var sumTransactions = _transactions.Where(t => t.PurseId == purseId).Sum(t => t.Amount);
            return purse.StartBalance + sumTransactions;
        }

        public TransactionDB GetTransactionById(Guid transactionId)
        {
            var transaction = _transactions.FirstOrDefault(t => t.Id == transactionId);
            return transaction is null ? null : new TransactionDB(transaction.Id, transaction.PurseId, transaction.Amount, transaction.Category, transaction.Date, transaction.Description);
        }
    }
}