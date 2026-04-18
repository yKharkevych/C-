using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels.Transactions
{
    // DTO for transferring information about a transaction when editing an existing transaction in EditTransactionPage
    public class TransactionEditDTO
    {
        public Guid Id { get; }
        public decimal Amount { get; }
        public Category Category { get; }
        public DateTime Date { get; }
        public string Description { get; }

        public TransactionEditDTO(Guid id, decimal amount, Category category, DateTime date, string description)
        {
            Id = id;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}
