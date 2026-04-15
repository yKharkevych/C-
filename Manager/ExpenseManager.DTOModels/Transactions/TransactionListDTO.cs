using Manager.ExpenseManager.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.DBModels.Transactions
{
    // DTO for transferring basic information about a transaction for displaying in the list of transactions on PurseDetailsPage
    public class TransactionListDTO
    {
        public Guid Id { get; } 
        public Category Category { get; }
        public string AmountDesc { get; }

        public TransactionListDTO(Guid id, Category category, decimal amount)
        {
            Id = id; 
            Category = category;
            AmountDesc = amount < 0 ? $"{amount}" : $"+{amount}";
        }
    }
}
