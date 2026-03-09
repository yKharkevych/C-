using Manager.ExpenseManager.Common;
using Manager.ExpenseManager.DBModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.Services
{
    internal static class FakeStorage
    {
        private static readonly List<PurseDB> _purses;
        private static readonly List<TransactionDB> _transactions;

        internal static IEnumerable<PurseDB> Purses
        {
            get { 
                return _purses.ToList();
            } 
        }

        internal static IEnumerable<TransactionDB> Transactions
        {
            get { 
                return _transactions.ToList();
            }
        }

        static FakeStorage()
        {
            var cash = new PurseDB("Готівка", Currency.UAH, 1000);
            var monoUAH = new PurseDB("Монобанк (UAH)", Currency.UAH, 12645);
            var monoUSD = new PurseDB("Монобанк (USD)", Currency.USD, 580);
            var cashPLN = new PurseDB("Готівка (PLN)", Currency.PLN, 1730.50m);
            _purses = new List<PurseDB> {cash, monoUAH, monoUSD, cashPLN };

            var t1 = new TransactionDB(cash.Id, -50, Category.Cafe, new DateTime(2026,3,7,15,34,56), "Купівля кави");
            var t2 = new TransactionDB(monoUAH.Id, 25000, Category.Salary, new DateTime(2026, 3, 1, 14, 25, 46), "Дохід з проєкту");
            var t3 = new TransactionDB(monoUAH.Id, -1200, Category.Clothes, new DateTime(2026, 3, 4, 13, 36, 23), "Нові джинси");
            var t4 = new TransactionDB(monoUSD.Id, -18.5m, Category.Cafe, new DateTime(2026, 3, 1, 15, 46, 21), "Обід");
            var t5 = new TransactionDB(cashPLN.Id, -200, Category.Entertainment, new DateTime(2026, 3, 2, 18, 45, 21), "Квитки в кіно");
            var t6 = new TransactionDB(monoUSD.Id, 300, Category.Other, new DateTime(2026, 3, 5, 12, 54, 47), "Продаж старого телефону");
            var t7 = new TransactionDB(cashPLN.Id, -368, Category.Products, new DateTime(2026, 3, 1, 21, 43, 21), "");
            var t8 = new TransactionDB(monoUAH.Id, -500, Category.Transport, new DateTime(2026, 3, 5, 8, 40, 11), "Таксі");
            var t9 = new TransactionDB(monoUAH.Id, -1500, Category.House, new DateTime(2026, 3, 3, 13, 36, 23), "Комунальні послуги");
            var t10 = new TransactionDB(monoUAH.Id, -17500, Category.House, new DateTime(2026, 3, 4, 10, 15, 47), "Оренда");
            var t11 = new TransactionDB(monoUSD.Id, 650, Category.Salary, new DateTime(2026, 3, 3, 15, 34, 56), "Фріланс");
            var t12 = new TransactionDB(monoUSD.Id, -200, Category.Other, new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти");
            var t13 = new TransactionDB(monoUAH.Id, 8600, Category.Other, new DateTime(2026, 3, 4, 12, 54, 47), "Обмін валюти");
            _transactions = new List<TransactionDB> {t1, t2,t3,t4,t5, t6, t7, t8,t9,t10,t11,t12,t13 };
        }
    }
}
