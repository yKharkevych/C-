using CommunityToolkit.Mvvm.ComponentModel;
using Manager.ExpenseManager.DBModels.Transactions;
using Manager.ExpenseManager.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Manager.ExpenseManager.ViewModel
{
    // ViewModel for the TransactionDetailsPage, responsible for handling the logic and data for displaying transaction details.
    public partial class TransactionDetailsVM : ObservableObject, IQueryAttributable
    {
        private readonly ITransactionService _transactionService;

        [ObservableProperty]
        public partial TransactionDetailsDTO? CurrentTransaction { get; set; }

        public TransactionDetailsVM(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var transactionId = (Guid)query["TransactionId"];
            CurrentTransaction = _transactionService.GetTransactionDetails(transactionId);
        }
    }
}
