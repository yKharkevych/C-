using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Manager.ExpenseManager.DBModels.Transactions
{
    // DTO for transferring basic information about a transaction for displaying in the list of transactions on PurseDetailsPage
    public class TransactionListDTO
    {
        public Guid Id { get; } 
        public Category Category { get; }
        public string AmountDesc { get; }
        public DateTime Date { get; }
        public string DateDisplay { get; }

        public TransactionListDTO(Guid id, Category category, decimal amount, DateTime date)
        {
            Id = id; 
            Category = category;
            AmountDesc = amount < 0 ? $"{amount}" : $"+{amount}";
            Date = date;
            DateDisplay = date.ToString("MMM dd, yyyy", new CultureInfo("en-US"));
        }
    }
}
