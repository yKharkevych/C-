using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Manager.ExpenseManager.DBModels.Transactions
{
    // DTO for transferring detailed information about a transaction for TransactionDetailsPage
    public class TransactionDetailsDTO
    {
        public Guid Id { get; }
        public Category Category { get; }
        public decimal Amount { get; }
        public string AmountDesc { get; }
        public string DateDisplay { get; }
        public string Description { get; }

        public TransactionDetailsDTO(Guid id, Category category, decimal amount, DateTime date, string description)
        {
            Id = id;
            Category = category;
            Amount = amount;
            AmountDesc = amount < 0 ? $"{amount}" : $"+{amount}";
            DateDisplay = date.ToString("MMMM dd, yyyy", new CultureInfo("en-US"));
            Description = description;
        }
    }
}
