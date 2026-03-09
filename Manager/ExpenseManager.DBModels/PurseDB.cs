using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels
{
    public class PurseDB
    {
        public Guid Id { get; } // Унікальний ідентифікатор гаманця. Генерується автоматично при створенні нового гаманця і ніколи не змінюється
        public string Name { get; set; } // Назва гаманця. Вона може бути змінена користувачем.
        public Currency Currency { get; } // Валюта гаманця. Встановлюється при створенні гаманця і не може бути змінена пізніше, адже всі операції відбуваються саме з цією валютою.
        public decimal StartBalance { get; }
        public PurseDB()
        {

        }

        public PurseDB(string name, Currency currency, decimal startBalance)
        {
            Id = Guid.NewGuid(); // Генеруємо унікальний ідентифікатор при створенні нового гаманця
            Name = name;
            Currency = currency;
            StartBalance = startBalance;
        }
    }
}
