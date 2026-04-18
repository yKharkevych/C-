using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels.Transactions
{
    // DTO for transferring information about a transaction when creating a new transaction in CreateTransactionPage
    public class TransactionCreateDTO
    {
        public Guid PurseId { get; }
        public decimal Amount { get; }
        public Category Category { get; }
        public DateTime Date { get; }
        public string Description { get; }

        public TransactionCreateDTO(Guid purseId, decimal amount, Category category, DateTime date, string description)
        {
            PurseId = purseId;
            Amount = amount;
            Category = category;
            Date = date;
            Description = description;
        }
    }
}
