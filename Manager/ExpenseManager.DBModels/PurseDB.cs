using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels
{
    public class PurseDB
    {
        public Guid Id { get; set; } // Унікальний ідентифікатор гаманця. Генерується автоматично при створенні нового гаманця і ніколи не змінюється
        public string Name { get; set; } // Назва гаманця. Вона може бути змінена користувачем.
        public Currency Currency { get; set; } // Валюта гаманця. Встановлюється при створенні гаманця і не може бути змінена пізніше, адже всі операції відбуваються саме з цією валютою.
        public decimal StartBalance { get; set; }
        

        public PurseDB()
        {

        }
        public PurseDB(string name, Currency currency, decimal startBlance) : this(Guid.NewGuid(), name, currency, startBlance)
        {

        }

        public PurseDB(Guid id, string name, Currency currency, decimal startBalance)
        {
            Id = id; 
            Name = name;
            Currency = currency;
            StartBalance = startBalance;
        }
    }
}
