using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DTOModels.Purses
{
    // DTO for transferring detailed information about a purse for PursesPage
    public class PurseDetailsDTO
    {
        public Guid Id { get; } // Унікальний ідентифікатор гаманця. Генерується автоматично при створенні нового гаманця і ніколи не змінюється
        public string Name { get; } // Назва гаманця. Вона може бути змінена користувачем.
        public Currency Currency { get; } // Валюта гаманця. Встановлюється при створенні гаманця і не може бути змінена пізніше, адже всі операції відбуваються саме з цією валютою.
        public decimal Balance { get; } // Поточний баланс гаманця. Він може змінюватися при додаванні або видаленні транзакцій.

        public PurseDetailsDTO(Guid id, string name, Currency currency, decimal balance)
        {
            Id = id; 
            Name = name;
            Currency = currency;
            Balance = balance;
        }
    }
}
