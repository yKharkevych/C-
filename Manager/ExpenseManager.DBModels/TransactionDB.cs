using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels
{
    public class TransactionDB
    {
        public Guid Id { get; } // Унікальний ідентифікатор транзакції. Генерується автоматично при створенні нової транзакції і ніколи не змінюється

        //Решту параметрів можна змінити, наприклад, коли були вказані не правильні дані або коли користувач хоче оновити інформацію про транзакцію.
        public Guid PurseId { get; }
        public decimal Amount { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }    

        public TransactionDB()
        {
        }

        public TransactionDB(Guid purseId, decimal amount, Category category, DateTime date, string description)
        {
            Id = Guid.NewGuid(); // Генеруємо унікальний ідентифікатор при створенні нової транзакції
            PurseId = purseId;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}
